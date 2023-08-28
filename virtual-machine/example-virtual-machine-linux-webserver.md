# example-azure / virtual-machine

```powershell
# crear ssh keys
"n" | ssh-keygen -t rsa -b 4096 -C "azureadministrator" -f "$ENV:UserProfile/.ssh/lcs16-vm-ubuntu" -P "azureprueba123*"

# crear virtual machine ubuntu
az vm create --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --location "eastus2" --image "Canonical:0001-com-ubuntu-server-jammy:22_04-lts-gen2:latest" --size "Standard_B2ms" --admin-username "azureadministrator" --ssh-key-values "~/.ssh/lcs16-vm-ubuntu.pub" --os-disk-size-gb 32 --public-ip-sku "Standard" --public-ip-address-dns-name "lcs16-vm-ubuntu"

# habilitar auto-shutdown
az vm auto-shutdown --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --time 0000

# habilitar puerto 80
az vm open-port --port 80 --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg"

# instalar aplicación
$result = Invoke-AzVMRunCommand -ResourceGroupName 'lcs16-rg' -Name 'lcs16-vm-ubuntu' -CommandId 'RunShellScript' -ScriptPath '.\virtual-machine\example-virtual-webserver-webserver-linux-script.sh'
Write-Output $result.Value

#https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx
```
