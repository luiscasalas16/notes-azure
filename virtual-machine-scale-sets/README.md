# example-azure / virtual-machine-scale-sets

[Azure Virtual Machine Scale Sets Documentation](https://learn.microsoft.com/en-us/azure/virtual-machine-scale-sets)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Virtual Machine Scale Sets.

```powershell
# crear virtual machine scale set windows
az vmss create --name "lcs16-vmss-win" --resource-group "lcs16-rg" --orchestration-mode "flexible" --image "MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition-smalldisk:latest" --vm-sku "Standard_B2ms" --admin-username "azureadministrator" --admin-password "azureprueba123*" --os-disk-size-gb 32 --instance-count 2

# crear virtual machine scale set linux
"n" | ssh-keygen -t rsa -b 4096 -C "azureadministrator" -f "$ENV:UserProfile/.ssh/lcs16-vmss-ubuntu" -P "azureprueba123*"
az vm create --name "lcs16-vmss-ubuntu" --resource-group "lcs16-rg" --orchestration-mode --image "Canonical:0001-com-ubuntu-server-jammy:22_04-lts-gen2:latest" --vm-sku "Standard_B2ms" --admin-username "azureadministrator" --ssh-key-values "~/.ssh/lcs16-vmss-ubuntu.pub" --os-disk-size-gb 32
```

```powershell
# listar scale set instances
az vmss list-instances --name "lcs16-vmss" --resource-group "lcs16-rg" --out table
# detalle scale set
az vmss show --name "lcs16-vmss" --resource-group "lcs16-rg"
# detalle scale set instance
az vmss show --name "lcs16-vmss" --resource-group "lcs16-rg" --instance-id "0"
```

```powershell
# escalar
az vmss scale --name "lcs16-vmss" --resource-group "lcs16-rg" --new-capacity 3
```

```powershell
# detener instancias
az vmss deallocate --name "lcs16-vmss" --resource-group "lcs16-rg"
# iniciar instancia
az vmss start --name "lcs16-vmss" --resource-group "lcs16-rg"
```

```powershell
# eliminar virtual machine scale set
az vmss delete --name "lcs16-vmss-win" --resource-group "lcs16-rg"
```
