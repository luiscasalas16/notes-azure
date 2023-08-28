@echo on
pwsh.exe -File .\BuildNet.ps1 "virtual-machine" "NetVirtualMachineWebMvc"
pwsh.exe -File .\BuildNetFw.ps1 "virtual-machine" "NetFwVirtualMachineWebMvc"
pause