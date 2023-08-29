@echo on

pwsh.exe -Command "& { . '.\Build.ps1'; BuildNet 'application-service' 'NetApplicationServiceWebMvc'; }"
pwsh.exe -Command "& { . '.\Build.ps1'; BuildNetFw 'application-service' 'NetFwApplicationServiceWebMvc'; }"
pwsh.exe -Command "& { . '.\Build.ps1'; BuildNet 'key-vault' 'NetKeyVaultWebMvc'; }"
pwsh.exe -Command "& { . '.\Build.ps1'; BuildNetFw 'key-vault' 'NetFwKeyVaultWebMvc'; }"
pwsh.exe -Command "& { . '.\Build.ps1'; BuildNet 'virtual-machine' 'NetVirtualMachineWebMvc'; }"
pwsh.exe -Command "& { . '.\Build.ps1'; BuildNetFw 'virtual-machine' 'NetFwVirtualMachineWebMvc'; }"
pwsh.exe -Command "& { . '.\Build.ps1'; BuildNet 'application-gateway' 'NetWafWebMvc'; }"

pause