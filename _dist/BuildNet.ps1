## Net

$folder=$args[0]
$proyect=$args[1]

# restore
Set-Location "..\$folder\$proyect"
dotnet restore
# publish
dotnet build -c Release
dotnet publish -c Release -o publish
# compress
Compress-Archive -Force -Path ".\publish\*" -DestinationPath "..\..\_dist\$proyect.zip"
Set-Location "..\..\_dist"
