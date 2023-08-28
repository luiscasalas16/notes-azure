## Net

# restore
Set-Location ..\application-service\NetApplicationServiceWebMvc
dotnet restore
# publish
dotnet build -c Release
dotnet publish -c Release -o publish
# compress
Compress-Archive -Force -Path ".\publish\*" -DestinationPath "..\..\_dist\NetApplicationServiceWebMvc.zip"
Set-Location ..\..\_dist

## NetFw

# commads
$installationPath = & "C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe" -prerelease -latest -property installationPath
if ($installationPath -and (test-path "$installationPath\Common7\Tools\vsdevcmd.bat")) {
  & "${env:COMSPEC}" /s /c "`"$installationPath\Common7\Tools\vsdevcmd.bat`" -no_logo && set" | foreach-object {
    $name, $value = $_ -split '=', 2
    set-content env:\"$name" $value
  }
}
# restore
Set-Location ..\application-service
MSBuild.exe /t:Restore /p:RestorePackagesConfig=true
# publish
Set-Location .\NetFwApplicationServiceWebMvc
MSBuild.exe /t:Rebuild /p:WebProjectOutputDir=".\publish" /p:OutDir=".\publish\bin" /p:Configuration=Release
# compress
Compress-Archive -Force -Path ".\publish\*" -DestinationPath "..\..\_dist\NetFwApplicationServiceWebMvc.zip"
Set-Location ..\..\_dist
