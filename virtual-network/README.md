# example-azure / virtual-network

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de una Virtual Network.

```powershell
# listar ips publicas de vms
az vm list-ip-addresses \
  --resource-group learn-559c1539-4ded-4e33-90c5-f354b40f38eb \
  --name my-vm \
  --query "[].virtualMachine.network.publicIpAddresses[*].ipAddress" \
  --output tsv
```

```powershell
# listar nsgs
az network nsg list \
  --resource-group learn-559c1539-4ded-4e33-90c5-f354b40f38eb \
  --query '[].name' \
  --output tsv
```

```powershell
# listar reglas de nsg
az network nsg rule list \
  --resource-group learn-559c1539-4ded-4e33-90c5-f354b40f38eb \
  --nsg-name my-vmNSG \
  --query '[].{Name:name, Priority:priority, Port:destinationPortRange, Access:access}' \
  --output table
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
