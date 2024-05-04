# example-azure / nat-gateway

[Azure Nat Gateway Documentation](https://learn.microsoft.com/en-us/azure/nat-gateway)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Nat Gateway

```powershell
# crear virtual network
az network vnet create --name "lcs16-vn" --resource-group "lcs16-rg" --address-prefix 10.0.0.0/16 --subnet-name "default" --subnet-prefixes 10.0.1.0/24

# crear ip
az network public-ip create --name "lcs16-ip-nat" --resource-group "lcs16-rg" --sku "Standard"

# crear nat gateway
az network nat gateway create --name "lcs16-nat" --resource-group "lcs16-rg" --public-ip-addresses "lcs16-ip-nat" --idle-timeout 5

# establecer nat gateway
az network vnet subnet update --name "default" --vnet-name "lcs16-vn" --resource-group "lcs16-rg" --nat-gateway "lcs16-nat"
```
