# example-azure / application-gateway

[Azure Application Gateway](https://learn.microsoft.com/en-us/azure/application-gateway)
[Azure Web Application Firewall Documentation](https://learn.microsoft.com/en-us/azure/web-application-firewall)

- [Comandos](#comandos)

---

## TODO

https://learn.microsoft.com/en-us/azure/application-gateway/tutorial-multiple-sites-cli
https://learn.microsoft.com/en-us/azure/application-gateway/tutorial-url-route-cli
https://learn.microsoft.com/en-us/azure/web-application-firewall/ag/tutorial-restrict-web-traffic-cli
https://learn.microsoft.com/en-us/azure/web-application-firewall/ag/configure-waf-custom-rules
https://learn.microsoft.com/en-us/azure/web-application-firewall/afds/waf-front-door-policy-configure-bot-protection?pivots=cli
https://learn.microsoft.com/en-us/azure/web-application-firewall/afds/waf-front-door-configure-ip-restriction#configure-a-waf-policy-with-the-azure-cli
https://learn.microsoft.com/en-us/azure/web-application-firewall/afds/waf-front-door-tutorial-geo-filtering

## Comandos

Comandos generales para la administración de un Web Application Gateway.

```powershell
# crear waf-policy
az network application-gateway waf-policy create --name "lcs16-wp" --resource-group "lcs16-rg" --location "eastus" --type "OWASP" --version "3.2"
# configurar waf policy
az network application-gateway waf-policy policy-setting update --policy-name "lcs16-wp" --resource-group "lcs16-rg" --state "Enabled" --mode "Prevention" --request-body-check true
```

```powershell
# crear ip
az network public-ip create --name "lcs16-ip-ag" --resource-group "lcs16-rg" --location "eastus" --version "IPv4" --sku "Standard" --allocation-method "Static" --zone 1 2 3 --tier "Regional" --dns-name "lcs16-ag"

# crear waf
az network application-gateway create --name "lcs16-ag" --resource-group "lcs16-rg" --location "eastus" --sku "WAF_v2" --waf-policy "lcs16-wp" --capacity 1 --http2 "Enabled" --vnet-name "lcs16-vn" --subnet "WafSubnet" --public-ip-address "lcs16-ip-ag" --servers lcs16-as-net-1.azurewebsites.net lcs16-as-net-2.azurewebsites.net --priority 1000 --http-settings-port 80 --http-settings-protocol "Http"

# eliminar waf
az network application-gateway delete --name "lcs16-ag" --resource-group "lcs16-rg"
```
