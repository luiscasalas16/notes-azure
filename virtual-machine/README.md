# Virtual Machine

## Comandos

Comandos generales para la administración de una Virtual Machine.

```powershell
# listar tamaños de vms
az vm list-sizes --location "eastus2" --output table
```

```powershell
# listar imagenes de vms
az vm image list --output table
```

```powershell
# crear virtual machine
az vm create --name "luiscasalas16vm" --resource-group "luiscasalas16-resource-group" --location "eastus2" --image "Win2019Datacenter" --size "Standard_B1ms" --admin-username "abc" --admin-password "123" --public-ip-sku "Standard" --public-ip-address-dns-name "luiscasalas16vm"

# habilitar auto-shutdown
az vm auto-shutdown --name "luiscasalas16vm" --resource-group "luiscasalas16-resource-group" --name "luiscasalas16vm" --time 0000
```

```powershell
# eliminar virtual machine
az vm delete --name "luiscasalas16vm" --resource-group "luiscasalas16-resource-group" --yes
```

```powershell
# instalar iis (https://learn.microsoft.com/en-us/powershell/module/servermanager)
az vm run-command invoke --name "luiscasalas16vm" --resource-group "luiscasalas16-resource-group" --command-id RunPowerShellScript --scripts "Install-WindowsFeature -name Web-Server"
az vm run-command invoke --name "luiscasalas16vm" --resource-group "luiscasalas16-resource-group" --command-id RunPowerShellScript --scripts "Install-WindowsFeature -name Web-Asp-Net45"
az vm run-command invoke --name "luiscasalas16vm" --resource-group "luiscasalas16-resource-group" --command-id RunPowerShellScript --scripts "Install-WindowsFeature -name Web-Mgmt-Console"

# habilitar puerto 80
az vm open-port --port 80 --name "luiscasalas16vm" --resource-group "luiscasalas16-resource-group"
```

```powershell
# listar zonas horarias disponibles
Get-TimeZone -ListAvailable
# establecer zona horaria Costa Rica
Set-TimeZone -Id "Central America Standard Time"
# obtener zona horaria
Get-TimeZone
```
