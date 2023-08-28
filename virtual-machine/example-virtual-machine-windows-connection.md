# example-virtual-machine-windows-connection

```powershell
# conectar virtual machine por remote desktop
cmdkey.exe /generic:"lcs16-vm-win.eastus.cloudapp.azure.com" /user:"azureadministrator" /pass:"azureprueba123*"
mstsc.exe /v:"lcs16-vm-win.eastus.cloudapp.azure.com"
cmdkey.exe /delete:"lcs16-vm-win.eastus.cloudapp.azure.com"
```
