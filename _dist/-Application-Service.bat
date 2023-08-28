@echo on
pwsh.exe -File .\BuildNet.ps1 "application-service" "NetApplicationServiceWebMvc"
pwsh.exe -File .\BuildNetFw.ps1 "application-service" "NetFwApplicationServiceWebMvc"
pause