# example-application-service-http

Ejemplo de configuración de un application gateway con waf cómo puerta de entrada para dos appservices.

- Se publica el application gateway por HTTP.
- Se distribuye el tráfico entre ambos appservices.
- Se permite sólo el tráfico desde el application gateway en los appservices.

```powershell
# crear resource group
az group create --name "lcs16-rg" --location "eastus2"

# crear appservice plan
az appservice plan create --name "lcs16-asp" --resource-group "lcs16-rg" --location "eastus2" `
    --is-linux --sku "B1"

# crear appservices
az webapp create --name "lcs16-as-net-1" --resource-group "lcs16-rg" `
    --plan "lcs16-asp" --runtime "DOTNETCORE:7.0"
az webapp create --name "lcs16-as-net-2" --resource-group "lcs16-rg" `
    --plan "lcs16-asp" --runtime "DOTNETCORE:7.0"

# compilar aplicación demo
cd .\application-service\NetApplicationServiceWebMvc
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish
Compress-Archive -Path ".\publish\*" -DestinationPath ".\publish.zip"

# publicar aplicación demo en appservices
az webapp deployment source config-zip --src .\publish.zip --name "lcs16-as-net-1" --resource-group "lcs16-rg"
az webapp deployment source config-zip --src .\publish.zip --name "lcs16-as-net-2" --resource-group "lcs16-rg"
cd ..\..\

# crear virtual network
az network vnet create --name "lcs16-vn" --resource-group "lcs16-rg" `
    --address-prefix 10.0.0.0/16
# crear virtual network subnet
az network vnet subnet create --vnet-name "lcs16-vn" --resource-group "lcs16-rg" `
    --name "agSubnet" --address-prefixes 10.0.1.0/24

# crear waf policy
az network application-gateway waf-policy create --name "lcs16-wp" --resource-group "lcs16-rg" `
    --location "eastus2" --type "OWASP" --version "3.2"
# configurar waf policy
az network application-gateway waf-policy policy-setting update --policy-name "lcs16-wp" --resource-group "lcs16-rg" `
    --state "Enabled" --mode "Prevention" --request-body-check true

# crear ip para application-gateway
az network public-ip create --name "lcs16-ip-ag" --resource-group "lcs16-rg" --location "eastus2" `
    --version "IPv4" --sku "Standard" --allocation-method "Static" --zone 1 2 3 --tier "Regional" --dns-name "lcs16-ag"

# crear application gateway
az network application-gateway create --name "lcs16-ag" --resource-group "lcs16-rg" --location "eastus2" `
    --sku "WAF_v2" --waf-policy "lcs16-wp" --capacity 1 --http2 "Enabled" `
    --vnet-name "lcs16-vn" --subnet "agSubnet" --public-ip-address "lcs16-ip-ag" `
    --servers lcs16-as-net-1.azurewebsites.net lcs16-as-net-2.azurewebsites.net --priority 1000 --http-settings-port 80 --http-settings-protocol "Http" `
    --frontend-port 80

# configurar backend setting application gateway
az network application-gateway http-settings update --gateway-name "lcs16-ag" --resource-group "lcs16-rg" `
    --name "appGatewayBackendHttpSettings" --port 443 --protocol "Https" --host-name-from-backend-pool true

# restringir tráfico de appservices
az webapp config access-restriction add --name "lcs16-as-net-1" --resource-group "lcs16-rg" `
    --rule-name "gateway-access" --priority 1000 --subnet "agSubnet" --vnet-name "lcs16-vn"
az webapp config access-restriction add --name "lcs16-as-net-2" --resource-group "lcs16-rg" `
    --rule-name "gateway-access" --priority 1000 --subnet "agSubnet" --vnet-name "lcs16-vn"

# liberar tráfico de appservices
az webapp config access-restriction remove --name "lcs16-as-net-1" --resource-group "lcs16-rg" --rule-name "gateway-access"
az webapp config access-restriction remove --name "lcs16-as-net-2" --resource-group "lcs16-rg" --rule-name "gateway-access"

#limpiar recursos
az group delete --name "lcs16-rg"

#https://learn.microsoft.com/en-us/azure/application-gateway/quick-create-portal
#https://learn.microsoft.com/en-us/azure/application-gateway/configure-web-app
#https://learn.microsoft.com/en-us/azure/app-service/networking/app-gateway-with-service-endpoints
```
