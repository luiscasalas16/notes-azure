# example-application-gateway-application-service-https

Ejemplo de configuración de un application gateway con waf cómo puerta de entrada para un application service con 2 instancias.

- Se publica el application gateway por HTTPS.
- Se distribuye el tráfico entre las instancias del application service.
- Se permite sólo el tráfico desde el application gateway en el application service.
- Se utiliza TLS termination en el application gateway.
- Se realiza la redirección de HTTP a HTTPS.

```powershell

##### application service #####

# crear application service plan
az appservice plan create --name "lcs16-asp" --resource-group "lcs16-rg" --location "eastus" `
    --is-linux --sku "P1V2" --number-of-workers 2

# crear application service
az webapp create --name "lcs16-as" --resource-group "lcs16-rg" `
    --plan "lcs16-asp" --runtime "DOTNETCORE:7.0"

# publicar aplicación en application service
az webapp deploy --name "lcs16-as" --resource-group "lcs16-rg" --src-path ".\_dist\NetApplicationServiceWebMvc.zip" --type "zip" --restart

##### virtual network #####

# crear virtual network
az network vnet create --name "lcs16-vn" --resource-group "lcs16-rg" `
    --address-prefix 10.0.0.0/16
# crear virtual network subnet
az network vnet subnet create --vnet-name "lcs16-vn" --resource-group "lcs16-rg" `
    --name "agSubnet" --address-prefixes 10.0.1.0/24

##### application-gateway #####

# crear waf policy
az network application-gateway waf-policy create --name "lcs16-wp" --resource-group "lcs16-rg" `
    --location "eastus" --type "OWASP" --version "3.2"
# configurar waf policy
az network application-gateway waf-policy policy-setting update --policy-name "lcs16-wp" --resource-group "lcs16-rg" `
    --state "Enabled" --mode "Prevention" --request-body-check true

# crear ip para application-gateway
az network public-ip create --name "lcs16-ip-ag" --resource-group "lcs16-rg" --location "eastus" `
    --version "IPv4" --sku "Standard" --allocation-method "Static" --tier "Regional" --dns-name "lcs16-application-gateway"

# crear application gateway
az network application-gateway create --name "lcs16-ag" --resource-group "lcs16-rg" --location "eastus" `
    --sku "WAF_v2" --waf-policy "lcs16-wp" --capacity 1 --http2 "Enabled" `
    --vnet-name "lcs16-vn" --subnet "agSubnet" --public-ip-address "lcs16-ip-ag" `
    --servers "lcs16-as.azurewebsites.net" --priority 1000 --http-settings-port 80 --http-settings-protocol "Http" `
    --frontend-port 443 --cert-file ".\application-gateway\lcs16-application-gateway.pfx" --cert-password "password"

# configurar backend setting application gateway
az network application-gateway http-settings update --gateway-name "lcs16-ag" --resource-group "lcs16-rg" `
    --name "appGatewayBackendHttpSettings" --port 443 --protocol "Https" --host-name-from-backend-pool true

# configurar redirección de http a https
az network application-gateway frontend-port create --gateway-name "lcs16-ag" --resource-group "lcs16-rg" `
    --name "httpPort" --port 80
az network application-gateway http-listener create --gateway-name "lcs16-ag" --resource-group "lcs16-rg" `
    --name "appGatewayHttpToHttpsListener" --frontend-ip "appGatewayFrontendIP" --frontend-port "httpPort"
az network application-gateway redirect-config create --gateway-name "lcs16-ag" --resource-group "lcs16-rg" `
    --name "httpToHttps" --target-listener "appGatewayHttpListener" --type "Permanent" --include-path true --include-query-string true
az network application-gateway rule create --gateway-name "lcs16-ag" --resource-group "lcs16-rg" `
    --name "rule2" --http-listener "appGatewayHttpToHttpsListener" --redirect-config httpToHttps --rule-type "Basic" --priority 1001

# restringir tráfico de application service
az webapp config access-restriction add --name "lcs16-as" --resource-group "lcs16-rg" `
    --rule-name "gateway-access" --priority 1000 --subnet "agSubnet" --vnet-name "lcs16-vn"

# liberar tráfico de application service
az webapp config access-restriction remove --name "lcs16-as" --resource-group "lcs16-rg" --rule-name "gateway-access"

```
