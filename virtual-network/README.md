# example-azure / virtual-network

[Azure Virtual Network Documentation](https://learn.microsoft.com/en-us/azure/virtual-network)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de una Virtual Network.

```powershell
# crear virtual network
az network vnet create --name "lcs16-vn" --resource-group "lcs16-rg" --address-prefix 10.0.0.0/16 --subnet-name "default" --subnet-prefixes 10.0.1.0/24
```

```powershell
# listar ips publicas de vms
az vm list-ip-addresses \
  --resource-group learn-559c1539-4ded-4e33-90c5-f354b40f38eb \
  --name my-vm \
  --query "[].virtualMachine.network.publicIpAddresses[*].ipAddress" \
  --out tsv
```

```powershell
# listar nsgs
az network nsg list \
  --resource-group learn-559c1539-4ded-4e33-90c5-f354b40f38eb \
  --query '[].name' \
  --out tsv
```

```powershell
# listar reglas de nsg
az network nsg rule list \
  --resource-group learn-559c1539-4ded-4e33-90c5-f354b40f38eb \
  --nsg-name my-vmNSG \
  --query '[].{Name:name, Priority:priority, Port:destinationPortRange, Access:access}' \
  --out table
```

```powershell
# crear regla en nsg
az network nsg rule create \
  --resource-group learn-559c1539-4ded-4e33-90c5-f354b40f38eb \
  --nsg-name my-vmNSG \
  --name allow-http \
  --protocol tcp \
  --priority 100 \
  --destination-port-range 80 \
  --access Allow
```
