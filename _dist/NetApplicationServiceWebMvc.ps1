Set-Location ..\application-service\NetApplicationServiceWebMvc
dotnet restore
dotnet build -c Release
dotnet publish -c Release -o publish
Compress-Archive -Force -Path ".\publish\*" -DestinationPath "..\..\_dist\NetApplicationServiceWebMvc.zip"