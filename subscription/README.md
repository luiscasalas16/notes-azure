# example-azure / subscription

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Resource Group.

```powershell
# ver suscripción actual
az account show
# listar suscripciones
az account list --query '[].{ Name:name, Id:id,TenantId:tenantId, IsDefault:isDefault }' --out table
# establecer suscripción actual
az account set --subscription "8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb"
az account set --name "Visual Studio Premium"

az account set --name "Visual Studio Enterprise - 1"
az account set --name "Visual Studio Enterprise - 2"
```
