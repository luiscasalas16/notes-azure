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
