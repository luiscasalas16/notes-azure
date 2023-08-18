# example-azure / virtual-machine

[Azure Virtual Machine Documentation](https://learn.microsoft.com/en-us/azure/virtual-machines)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de una Virtual Machine.

```powershell
# listar tamaños de vms
az vm list-sizes --location "eastus2" --outtable
```

```powershell
# listar imágenes de vms
az vm image list --out table
# buscar imágenes de vms
az vm image list --offer WindowsServer --sku 2022-datacenter --all
# imágenes Windows Server 2022:
#  MicrosoftWindowsServer:WindowsServer:2022-datacenter-g2:latest
#  MicrosoftWindowsServer:WindowsServer:2022-datacenter-smalldisk-g2:latest
#  MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition:latest
#  MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition-smalldisk:latest
```

```powershell
# crear virtual machine windows
az vm create --name "lcs16-vm-win" --resource-group "lcs16-rg" --location "eastus2" --image "MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition-smalldisk:latest" --size "Standard_B2ms" --admin-username "azureadministrator" --admin-password "azureprueba123*" --public-ip-sku "Standard" --public-ip-address-dns-name "lcs16-vm-win" --os-disk-size-gb 64

# crear virtual machine ubuntu
az vm create --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --location "eastus2" --image "UbuntuLTS" --size "Standard_B2ms" --admin-username "azureadministrator" --generate-ssh-keys --public-ip-sku "Standard" --public-ip-address-dns-name "lcs16-vm-ubuntu" --os-disk-size-gb 64

# habilitar auto-shutdown
az vm auto-shutdown --name "lcs16-vm-win" --resource-group "lcs16-rg" --time 0000
az vm auto-shutdown --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --time 0000
```

```powershell
# eliminar virtual machine
az vm delete --name "lcs16-vm-win" --resource-group "lcs16-rg"
```

```powershell
# asignar system-assigned identity a virtual machine
az vm identity assign --name "lcs16-vm-win" --resource-group "lcs16-rg"
# eliminar system-assigned identity a virtual machine
az vm identity remove --name "lcs16-vm-win" --resource-group "lcs16-rg"
```

```powershell
# asignar user-assigned identity a virtual machine
az vm identity assign --name "lcs16-vm-win" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
# eliminar user-assigned identity a virtual machine
az vm identity remove --name "lcs16-vm-win" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
```

```powershell
# instalar webserver en windows
az vm run-command invoke --name "lcs16-vm-win" --resource-group "lcs16-rg" --command-id RunPowerShellScript --scripts "Install-WindowsFeature -name Web-Server"
az vm run-command invoke --name "lcs16-vm-win" --resource-group "lcs16-rg" --command-id RunPowerShellScript --scripts "Install-WindowsFeature -name Web-Asp-Net45"
az vm run-command invoke --name "lcs16-vm-win" --resource-group "lcs16-rg" --command-id RunPowerShellScript --scripts "Install-WindowsFeature -name Web-Mgmt-Console"

# instalar webserver en ubuntu
az vm run-command invoke --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --command-id RunShellScript --scripts "sudo apt-get update && sudo apt-get install -y nginx"

# habilitar puerto 80
az vm open-port --port 80 --name "lcs16-vm-win" --resource-group "lcs16-rg"
az vm open-port --port 80 --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg"
```

```powershell
# listar zonas horarias disponibles
Get-TimeZone -ListAvailable
# establecer zona horaria Costa Rica
Set-TimeZone -Id "Central America Standard Time"
# obtener zona horaria
Get-TimeZone
```
