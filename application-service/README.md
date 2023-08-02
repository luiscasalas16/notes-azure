# Application Service

## Comandos

Comandos generales para la administración de un Application Service.

```powershell
#crear application service plan
az appservice plan create --name "luiscasalas16-app-service-plan" --resource-group "luiscasalas16-resource-group" --location "eastus2" --sku "F1"
```

```powershell
#crear application service plan
az appservice plan delete --name "luiscasalas16-app-service-plan" --resource-group "luiscasalas16-resource-group"
```

```powershell
#crear application service .Net
az webapp create --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group" --plan "luiscasalas16-app-service-plan" --runtime "dotnet:7"
```

```powershell
#crear application service .Net Framework
az webapp create --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group" --plan "luiscasalas16-app-service-plan" --runtime "ASPNET:V4.8"
```
