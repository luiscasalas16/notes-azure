# example-azure / application-service

- [Comandos Application Service Plan](#comandos-application-service-plan)
- [Comandos Application Service](#comandos-application-service)

---

## Comandos Application Service Plan

Comandos generales para la administración de un Application Service Plan.

```powershell
# crear application service plan
az appservice plan create --name "luiscasalas16-app-service-plan" --resource-group "luiscasalas16-resource-group" --location "eastus2" --sku "F1"
```

```powershell
# eliminar application service plan
az appservice plan delete --name "luiscasalas16-app-service-plan" --resource-group "luiscasalas16-resource-group"
```

---

## Comandos Application Service

Comandos generales para la administración de un Application Service.

```powershell
# crear application service .Net
az webapp create --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group" --plan "luiscasalas16-app-service-plan" --runtime "dotnet:7"
# crear application service .Net Framework
az webapp create --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group" --plan "luiscasalas16-app-service-plan" --runtime "ASPNET:V4.8"
```

```powershell
# eliminar application service .Net
az webapp delete --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group" --keep-empty-plan
# eliminar application service .Net Framework
az webapp delete --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group" --keep-empty-plan
```

```powershell
# asignar system-assigned identity a application service
az webapp identity assign --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group"
az webapp identity assign --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group"
# eliminar system-assigned identity a application service
az webapp identity remove --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group"
az webapp identity remove --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group"
```

```powershell
# asignar user-assigned identity a application service
az webapp identity assign --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/luiscasalas16-resource-group/providers/Microsoft.ManagedIdentity/userAssignedIdentities/luiscasalas16-managed-identity"
az webapp identity assign --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/luiscasalas16-resource-group/providers/Microsoft.ManagedIdentity/userAssignedIdentities/luiscasalas16-managed-identity"
# eliminar user-assigned identity a application service
az webapp identity remove --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/luiscasalas16-resource-group/providers/Microsoft.ManagedIdentity/userAssignedIdentities/luiscasalas16-managed-identity"
az webapp identity remove --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/luiscasalas16-resource-group/providers/Microsoft.ManagedIdentity/userAssignedIdentities/luiscasalas16-managed-identity"
```
