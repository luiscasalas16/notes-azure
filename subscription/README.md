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
```
