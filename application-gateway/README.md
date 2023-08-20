# example-azure / application-gateway

[Azure Application Gateway](https://learn.microsoft.com/en-us/azure/application-gateway)
[Azure Web Application Firewall Documentation](https://learn.microsoft.com/en-us/azure/web-application-firewall)

- [Comandos](#comandos)

- [](https://azure.github.io/Azure-Proactive-Resiliency-Library/services/networking/web-application-firewall)
- [](https://tutorialsdojo.com/azure-application-gateway)

content-based routing,
ability to host multiple websites,
security enhancements.
Geo-filtering
IP restriction
Rate limiting

---

## Comandos

Comandos generales para la administración de un Web Application Gateway.

```powershell
# crear waf-policy
az network application-gateway waf-policy create --name "lcs16-wp" --resource-group "lcs16-rg" --location "eastus2" --type "OWASP" --version "3.2"
# configurar waf policy
az network application-gateway waf-policy policy-setting update --policy-name "lcs16-wp" --resource-group "lcs16-rg" --state "Enabled" --mode "Prevention" --request-body-check true
```

```powershell
# crear ip
az network public-ip create --name "lcs16-ip-ag" --resource-group "lcs16-rg" --location "eastus2" --version "IPv4" --sku "Standard" --allocation-method "Static" --zone 1 2 3 --tier "Regional" --dns-name "lcs16-ag"

# crear waf
az network application-gateway create --name "lcs16-ag" --resource-group "lcs16-rg" --location "eastus2" --sku "WAF_v2" --waf-policy "lcs16-wp" --capacity 1 --http2 "Enabled" --vnet-name "lcs16-vn" --subnet "WafSubnet" --public-ip-address "lcs16-ip-ag" --servers lcs16-as-net-1.azurewebsites.net lcs16-as-net-2.azurewebsites.net --priority 1000 --http-settings-port 80 --http-settings-protocol "Http"

# eliminar waf
az network application-gateway delete --name "lcs16-ag" --resource-group "lcs16-rg"
```
