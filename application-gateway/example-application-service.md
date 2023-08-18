```powershell
az group create --name "lcs16-rg" --location "eastus2"

az appservice plan create --name "lcs16-asp" --resource-group "lcs16-rg" --location "eastus2" --sku "B1" --is-linux

az webapp create --name "lcs16-as-net-1" --resource-group "lcs16-rg" --plan "lcs16-asp" --runtime "DOTNETCORE:7.0"
az webapp create --name "lcs16-as-net-2" --resource-group "lcs16-rg" --plan "lcs16-asp" --runtime "DOTNETCORE:7.0"

az network vnet create --name "lcs16-vn-agap" --resource-group "lcs16-rg" --address-prefix 10.0.0.0/16
az network vnet subnet create --vnet-name "lcs16-vn-agap" --resource-group "lcs16-rg" -n "AgSubnet" --address-prefixes 10.0.1.0/24

az network application-gateway waf-policy create --name "lcs16-wp-agap" --resource-group "lcs16-rg" --location "eastus2" --type "OWASP" --version "3.2"
az network application-gateway waf-policy policy-setting update --policy-name "lcs16-wp-agap" --resource-group "lcs16-rg" --state "Enabled" --mode "Prevention" --request-body-check true

az network public-ip create --name "lcs16-ip-agap" --resource-group "lcs16-rg" --location "eastus2" --version "IPv4" --sku "Standard" --allocation-method "Static" --zone 1 2 3 --tier "Regional" --dns-name "lcs16-ag-agap"

az network application-gateway create --name "lcs16-ag-agap" --resource-group "lcs16-rg" --location "eastus2" --sku "WAF_v2" --waf-policy "lcs16-wp-agap" --capacity 1 --http2 "Enabled" --vnet-name "lcs16-vn-agap" --subnet "AgSubnet" --public-ip-address "lcs16-ip-agap" --servers lcs16-as-net-1.azurewebsites.net lcs16-as-net-2.azurewebsites.net --priority 1000 --http-settings-port 80 --http-settings-protocol "Http"

az network application-gateway http-settings update --name "appGatewayBackendHttpSettings" --gateway-name "lcs16-ag-agap" --resource-group "lcs16-rg" --port 443 --protocol "Https" --host-name-from-backend-pool true

#az group delete --name "lcs16-rg"
```
