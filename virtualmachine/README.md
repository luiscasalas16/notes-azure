# virtualmachine

Cambio de zona horaria de máquina virtual en azure a Costa Rica:

```powershell
Set-TimeZone -Id "Central America Standard Time"
Get-TimeZone
```

Cambio de zona horaria de máquina virtual en azure según corresponda:

```powershell
Get-TimeZone -ListAvailable
Get-TimeZone -ListAvailable | where ({$_.Id -like "Pacific*"})
Set-TimeZone -Id "Pacific Standard Time"
```
