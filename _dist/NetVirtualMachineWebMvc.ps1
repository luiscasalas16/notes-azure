Set-Location ..\virtual-machine\NetVirtualMachineWebMvc
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish
Compress-Archive -Force -Path ".\publish\*" -DestinationPath "..\..\_dist\NetVirtualMachineWebMvc.zip"