# example-azure / automation

[Azure Automation Documentation](https://learn.microsoft.com/en-us/azure/automation/)

## Creación Runbook

1. Crear "Automation Account", utilizando una "Managed identity" "System assigned".
2. Otorgar los permisos correspondientes a la "Managed identity" del "Automation Account" en el recurso correspondiente, se puede considerar el rol de "Contributor" para evitar incovenientes de permisos.
3. Crear "Runbook", utilizando type "PowerShell" y version "7.2".
4. Editar script del "Runbook".
5. "Save" del "Runbook".
6. "Publish" del "Runbook".
7. Crear un "Schedule" para el "Runbook".
8. Referencia: [start-stop-virtual-machine](https://blog.matrixpost.net/azure-virtual-machine-start-stop-schedule/)

## Ejemplos Runbook

```powershell
#conectar a azure
try
{
    "conexion a Azure"
    Connect-AzAccount -Identity
}
catch {
    Write-Error "!!! ERROR en la conexion a Azure !!!"
    Write-Error $_.Exception.Message
    throw $_.Exception
}
```

```powershell
# encender máquina virtual
Write-Output "=== encender maquina virtual ==="
Start-AzVM -ResourceGroupName "resource group name" -Name "virtual machine name"
```

```powershell
# apagar máquina virtual
Write-Output "=== apagar maquina virtual ==="
Stop-AzVM -ResourceGroupName "resource group name" -Name "virtual machine name" -Force
```

```powershell
# verificar si máquina virtual está ejecutando
if ((Get-AzVM -ResourceGroupName "resource group name" -Name "virtual machine name" -Status).Statuses.Where({ $_.Code -like 'PowerState/*' }).DisplayStatus -eq 'VM running')
{

}
```

```powershell
# ejecutar script en máquina virtual en linux
try {
    Write-Output "=== ejecutar script ==="
    $result = Invoke-AzVMRunCommand `
        -ResourceGroupName 'resource group name' `
        -Name 'virtual machine name' `
        -CommandId 'RunShellScript' `
        -ScriptString 'script' `
        -ErrorAction Stop

    Write-Output "=== resultado script ==="

    foreach ($entry in $result.Value) {
        if ($entry.Message) {
            Write-Output $entry.Message.Trim()
        }
    }
}
catch {
    Write-Error "!!! ERROR en script !!!"
    Write-Error $_.Exception.Message
    throw $_.Exception
}
```
