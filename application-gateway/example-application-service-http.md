Ejemplo de configuración de un application-gateway con waf cómo punto de entrada para dos appservices.

```powershell
# crear resource group
az group create --name "lcs16-rg" --location "eastus2"

# crear appservice plan
az appservice plan create --name "lcs16-asp" --resource-group "lcs16-rg" --location "eastus2"
    \ --is-linux --sku "B1"

# crear appservices
az webapp create --name "lcs16-as-net-1" --resource-group "lcs16-rg" \
    --plan "lcs16-asp" --runtime "DOTNETCORE:7.0"
az webapp create --name "lcs16-as-net-2" --resource-group "lcs16-rg" \
    --plan "lcs16-asp" --runtime "DOTNETCORE:7.0"
# restringir tráfico de appservices sólo desde application gateway
az webapp config access-restriction add --name "lcs16-as-net-1" --resource-group "lcs16-rg" \
    --priority 1000 --rule-name gateway-access --subnet "AgSubnet" --vnet-name "lcs16-vn-agap"
az webapp config access-restriction add --name "lcs16-as-net-2" --resource-group "lcs16-rg" \
    --priority 1000 --rule-name gateway-access --subnet "AgSubnet" --vnet-name "lcs16-vn-agap"

# crear virtual network
az network vnet create --name "lcs16-vn-agap" --resource-group "lcs16-rg" \
    --address-prefix 10.0.0.0/16
# crear virtual network subnet
az network vnet subnet create --vnet-name "lcs16-vn-agap" --resource-group "lcs16-rg" \
    -name "AgSubnet" --address-prefixes 10.0.1.0/24

# crear waf policy
az network application-gateway waf-policy create --name "lcs16-wp-agap" --resource-group "lcs16-rg" \
    --location "eastus2" --type "OWASP" --version "3.2"
# configurar waf policy
az network application-gateway waf-policy policy-setting update --policy-name "lcs16-wp-agap" --resource-group "lcs16-rg" \
    --state "Enabled" --mode "Prevention" --request-body-check true

# crear ip para application-gateway
az network public-ip create --name "lcs16-ip-agap" --resource-group "lcs16-rg" --location "eastus2" \
    --version "IPv4" --sku "Standard" --allocation-method "Static" --zone 1 2 3 --tier "Regional" --dns-name "lcs16-ag-agap"

# crear application gateway
az network application-gateway create --name "lcs16-ag-agap" --resource-group "lcs16-rg" --location "eastus2" \
    --sku "WAF_v2" --waf-policy "lcs16-wp-agap" --capacity 1 --http2 "Enabled" \
    --vnet-name "lcs16-vn-agap" --subnet "AgSubnet" --public-ip-address "lcs16-ip-agap" \
    --servers lcs16-as-net-1.azurewebsites.net lcs16-as-net-2.azurewebsites.net --priority 1000 --http-settings-port 80 --http-settings-protocol "Http" \
    --frontend-port 80

# configurar backend setting application gateway
az network application-gateway http-settings update --gateway-name "lcs16-ag-agap" --resource-group "lcs16-rg" \
    --name "appGatewayBackendHttpSettings" --port 443 --protocol "Https" --host-name-from-backend-pool true

#limpiar recursos
az group delete --name "lcs16-rg"

#https://learn.microsoft.com/en-us/azure/application-gateway/quick-create-portal
#https://learn.microsoft.com/en-us/azure/application-gateway/configure-web-app
#https://learn.microsoft.com/en-us/azure/application-gateway/tutorial-ssl-cli
#https://learn.microsoft.com/en-us/azure/app-service/networking/app-gateway-with-service-endpoints
```
