# example-application-gateway-application-service-http

Ejemplo de configuración de un application gateway con waf cómo puerta de entrada para un application service con 2 instancias.

- Se publica el application gateway por HTTP.
- Se distribuye el tráfico entre las instancias del application service.
- Se permite sólo el tráfico desde el application gateway en el application service.

```powershell

##### application service #####

# crear application service plan
az appservice plan create --name "ms-dev-asp" --resource-group "ms-dev-rg" --location "eastus" `
    --is-linux --sku "P1V2" --number-of-workers 2

# crear application service
az webapp create --name "ms-dev-as" --resource-group "ms-dev-rg" `
    --plan "ms-dev-asp" --runtime "DOTNETCORE:7.0"

# publicar aplicación en application service
az webapp deploy --name "ms-dev-as" --resource-group "ms-dev-rg" --src-path ".\_dist\NetApplicationServiceWebMvc.zip" --type "zip" --restart

##### network #####

# crear virtual network
az network vnet create --name "ms-dev-vn" --resource-group "ms-dev-rg" `
    --address-prefix 10.0.0.0/16
# crear virtual network subnet
az network vnet subnet create --vnet-name "ms-dev-vn" --resource-group "ms-dev-rg" `
    --name "agSubnet" --address-prefixes 10.0.1.0/24

# crear ip
az network public-ip create --name "ms-dev-ip-ag" --resource-group "ms-dev-rg" --location "eastus" `
    --version "IPv4" --sku "Standard" --allocation-method "Static" --tier "Regional" --dns-name "ms-dev-application-gateway"

##### waf-policy #####

# crear waf policy
az network application-gateway waf-policy create --name "ms-dev-wp" --resource-group "ms-dev-rg" `
    --location "eastus" --type "OWASP" --version "3.2"
# configurar waf policy
az network application-gateway waf-policy policy-setting update --policy-name "ms-dev-wp" --resource-group "ms-dev-rg" `
    --state "Enabled" --mode "Prevention" --request-body-check true

##### application-gateway #####

# crear application gateway
az network application-gateway create --name "ms-dev-ag" --resource-group "ms-dev-rg" --location "eastus" `
    --sku "WAF_v2" --waf-policy "ms-dev-wp" --capacity 1 --http2 "Enabled" `
    --vnet-name "ms-dev-vn" --subnet "agSubnet" --public-ip-address "ms-dev-ip-ag" `
    --priority 1000 --http-settings-port 80 --http-settings-protocol "Http" `
    --frontend-port 80

# backend pools
az network application-gateway address-pool create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" --name "rootBackendPool" --servers "ms-dev-vm.eastus.cloudapp.azure.com"

az network application-gateway address-pool create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" --name "retcBackendPool" --servers "ms-dev-vm.eastus.cloudapp.azure.com"

az network application-gateway address-pool create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" --name "reacahBackendPool" --servers "ms-dev-vm.eastus.cloudapp.azure.com"

# backend setting, /path -> /path
az network application-gateway http-settings create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" `
    --name "appGatewayBackendHttpsSettings" --port 443 --protocol "Https" --host-name-from-backend-pool true --path "/"
# backend setting, /path -> /
az network application-gateway http-settings create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" `
    --name "appGatewayBackendHttpsSettingsOverride" --port 443 --protocol "Https" --host-name-from-backend-pool true --path "/"

# port
az network application-gateway frontend-port create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" --name "appGatewayFrontendPort8080" --port 8080

# listeners
az network application-gateway http-listener create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" `
    --name "appGatewayHttpListener8080" --frontend-ip "appGatewayFrontendIP" --frontend-port "appGatewayFrontendPort8080"

# rules
az network application-gateway url-path-map create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" `
    --name "rootPathMap" --paths "/*" `
    --address-pool "rootBackendPool" --default-address-pool "rootBackendPool" `
    --http-settings "appGatewayBackendHttpSettings" --default-http-settings "appGatewayBackendHttpSettings" `
    --rule-name "rootPathRule"

az network application-gateway url-path-map rule create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" `
    --name "retcPathRule" --paths "/retc/*" --path-map-name "rootPathMap" --address-pool "retcBackendPool" --http-settings "appGatewayBackendHttpSettings"

az network application-gateway url-path-map rule create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" `
    --name "reacahPathRule" --paths "/reacah/*" --path-map-name "rootPathMap" --address-pool "reacahBackendPool" --http-settings "appGatewayBackendHttpSettings"

az network application-gateway rule create --gateway-name "ms-dev-ag" --resource-group "ms-dev-rg" `
    --name "rootRule" --rule-type "PathBasedRouting" --url-path-map "rootPathMap" `
    --address-pool "rootBackendPool" --http-listener "appGatewayHttpListener8080" `
    --priority 200

##### application service #####

# restringir tráfico de application service
az webapp config access-restriction add --name "ms-dev-as" --resource-group "ms-dev-rg" `
    --rule-name "gateway-access" --priority 1000 --subnet "agSubnet" --vnet-name "ms-dev-vn"

# liberar tráfico de application service
az webapp config access-restriction remove --name "ms-dev-as" --resource-group "ms-dev-rg" --rule-name "gateway-access"

```
