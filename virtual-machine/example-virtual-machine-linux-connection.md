```powershell
# conectar virtual machine por ssh
ssh -i ~/.ssh/lcs16-vm-ubuntu "azureadministrator@lcs16-vm-ubuntu.eastus2.cloudapp.azure.com"
```

```powershell
# compilar aplicación demo
cd .\virtual-machine\NetVirtualMachineWebMvc
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish

# publicar aplicación demo
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
```
