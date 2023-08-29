# instalar iis
Install-WindowsFeature -name Web-Server
Install-WindowsFeature -name Web-Mgmt-Console
Install-WindowsFeature -name Web-Scripting-Tools

# instalar .net
Invoke-WebRequest -Uri "https://dot.net/v1/dotnet-install.ps1" -OutFile ".\dotnet-install.ps1"
.\dotnet-install.ps1 -Runtime aspnetcore -Version 7.0.10 -InstallDir "C:\Program Files\dotnet"

# instalar .net hosting bundle
Invoke-WebRequest -Uri "https://download.visualstudio.microsoft.com/download/pr/d489c5d0-4d0f-4622-ab93-b0f2a3e92eed/101a2fae29a291956d402377b941f401/dotnet-hosting-7.0.10-win.exe
" -OutFile ".\windows-hosting-bundle.exe"
Start-Process ".\windows-hosting-bundle.exe" -Wait -ArgumentList '/quiet /install'
iisreset

# configurar application pool
Import-Module WebAdministration
Set-ItemProperty -Path "IIS:\AppPools\DefaultAppPool" -Name managedRuntimeVersion -Value ""

# instalar aplicación
Set-Location C:\inetpub\wwwroot
wget https://raw.githubusercontent.com/luiscasalas16/notes-azure/main/_dist/NetVirtualMachineWebMvc.zip -O NetVirtualMachineWebMvc.zip
Expand-Archive -Force NetVirtualMachineWebMvc.zip .
Remove-Item NetVirtualMachineWebMvc.zip
