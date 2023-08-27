# example-azure / virtual-machine

```powershell
# crear key file
"n" | ssh-keygen -t rsa -b 4096 -C "azureadministrator" -f "$ENV:UserProfile/.ssh/lcs16-vm-ubuntu" -P "azureprueba123*"

# crear virtual machine ubuntu
az vm create --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --location "eastus2" --image "Canonical:0001-com-ubuntu-server-jammy:22_04-lts-gen2:latest" --size "Standard_B1ms" --admin-username "azureadministrator" --ssh-key-values "~/.ssh/lcs16-vm-ubuntu.pub" --os-disk-size-gb 32 --public-ip-sku "Standard" --public-ip-address-dns-name "lcs16-vm-ubuntu"

# habilitar auto-shutdown
az vm auto-shutdown --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg" --time 0000

# habilitar puerto 80
az vm open-port --port 80 --name "lcs16-vm-ubuntu" --resource-group "lcs16-rg"

# ejecutar script 1
Invoke-AzVMRunCommand -ResourceGroupName 'lcs16-rg' -Name 'lcs16-vm-ubuntu' -CommandId 'RunShellScript' -ScriptPath '.\virtual-machine\example-virtual-machine-webserver-linux-script-1.sh'

# compilar aplicación demo
cd .\virtual-machine\NetVirtualMachineWebMvc
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish

# publicar aplicación demo en app service
& 'C:\Program Files (x86)\WinSCP\WinSCP.com' /keygen "$ENV:UserProfile/.ssh/lcs16-vm-ubuntu" /output="$ENV:UserProfile/.ssh/lcs16-vm-ubuntu.ppk" /passphrase="azureprueba123*"
& 'C:\Program Files (x86)\WinSCP\WinSCP.com' `
    /ini=nul `
    /command `
    "open sftp://azureadministrator@lcs16-vm-ubuntu.eastus2.cloudapp.azure.com/ -hostkey=* -privatekey=C:\Users\lsalas\.ssh\lcs16-vm-ubuntu.ppk -passphrase=azureprueba123*" `
    "lcd .\publish" `
    "cd /var/www/app" `
    "put *" `
    "exit"
cd ..\..\

# ejecutar script 2
Invoke-AzVMRunCommand -ResourceGroupName 'lcs16-rg' -Name 'lcs16-vm-ubuntu' -CommandId 'RunShellScript' -ScriptPath '.\virtual-machine\example-virtual-machine-webserver-linux-script-2.sh'

# conectar virtual machine por ssh
ssh -i ~/.ssh/lcs16-vm-ubuntu "azureadministrator@lcs16-vm-ubuntu.eastus2.cloudapp.azure.com"

#https://code-maze.com/deploy-aspnetcore-linux-nginx
#https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx
#https://winscp.net/eng/docs/commandline
```
