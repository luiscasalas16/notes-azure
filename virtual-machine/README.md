# example-azure / virtual-machine

[Azure Virtual Machine Documentation](https://learn.microsoft.com/en-us/azure/virtual-machines)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de una Virtual Machine.

```powershell
# listar tamaños de vms
az vm list-sizes --location "eastus" --out table
```

```powershell
# listar imágenes de vms
az vm image list --out table
# buscar imágenes de vms
az vm image list --offer WindowsServer --sku 2022-datacenter --all
# imágenes windows
#   MicrosoftWindowsServer:WindowsServer:2022-datacenter-g2:latest
#   MicrosoftWindowsServer:WindowsServer:2022-datacenter-smalldisk-g2:latest
#   MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition:latest
#   MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition-smalldisk:latest
# imágenes linux
#   Canonical:0001-com-ubuntu-server-jammy:22_04-lts-gen2:latest
#   Canonical:0001-com-ubuntu-pro-jammy:pro-22_04-lts-gen2:latest
```

```powershell
# crear virtual machine windows
az vm create --name "lcs16-vm-win" --resource-group "lcs16-rg" --location "eastus" --image "MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition-smalldisk:latest" --size "Standard_B2ms" --admin-username "azureadministrator" --admin-password "azureprueba123*" --os-disk-size-gb 32 --public-ip-sku "Standard" --public-ip-address-dns-name "lcs16-vm-win"

# crear virtual machine linux
"n" | ssh-keygen -t rsa -b 4096 -C "azureadministrator" -f "$ENV:UserProfile/.ssh/lcs16-vm-ubuntu" -P "azureprueba123*"
az vm create --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --location "eastus" --image "Canonical:0001-com-ubuntu-server-jammy:22_04-lts-gen2:latest" --size "Standard_B2ms" --admin-username "azureadministrator" --ssh-key-values "~/.ssh/lcs16-vm-ubuntu.pub" --os-disk-size-gb 32 --public-ip-sku "Standard" --public-ip-address-dns-name "lcs16-vm-ubuntu"
```

```powershell
# habilitar auto-shutdown
az vm auto-shutdown --name "lcs16-vm" --resource-group "lcs16-rg" --time 0000
```

```powershell
# detener instancias
az vm deallocate --name "lcs16-vm" --resource-group "lcs16-rg"
# iniciar instancia
az vm start --name "lcs16-vm" --resource-group "lcs16-rg"
```

```powershell
# eliminar virtual machine
az vm delete --name "lcs16-vm" --resource-group "lcs16-rg"
```

```powershell
# asignar system-assigned identity a virtual machine
az vm identity assign --name "lcs16-vm" --resource-group "lcs16-rg"
# eliminar system-assigned identity a virtual machine
az vm identity remove --name "lcs16-vm" --resource-group "lcs16-rg"
```

```powershell
# asignar user-assigned identity a virtual machine
az vm identity assign --name "lcs16-vm" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
# eliminar user-assigned identity a virtual machine
az vm identity remove --name "lcs16-vm" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
```

```powershell
# listar zonas horarias disponibles
Get-TimeZone -ListAvailable
# establecer zona horaria Costa Rica
Set-TimeZone -Id "Central America Standard Time"
# obtener zona horaria
Get-TimeZone
```
