# example-virtual-machine-windows-webserver

```powershell
# crear virtual machine windows
az vm create --name "lcs16-vm-win" --resource-group "lcs16-rg" --location "eastus" --image "MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition-smalldisk:latest" --size "Standard_B2ms" --admin-username "azureadministrator" --admin-password "azureprueba123*" --os-disk-size-gb 32 --public-ip-sku "Standard" --public-ip-address-dns-name "lcs16-vm-win"

# habilitar auto-shutdown
az vm auto-shutdown --name "lcs16-vm-win" --resource-group "lcs16-rg" --time 0000

# habilitar puerto 80
az vm open-port --port 80 --name "lcs16-vm-win" --resource-group "lcs16-rg"

# instalar aplicación
$result = Invoke-AzVMRunCommand -ResourceGroupName 'lcs16-rg' -Name 'lcs16-vm-win' -CommandId 'RunPowerShellScript' -ScriptPath '.\virtual-machine\example-virtual-machine-windows-webserver-script.ps1' -ErrorAction "Stop"
Write-Output $result.Value
```
