### App service plan

```cmd
::crear app service plan
az appservice plan create --name "luiscasalas16-app-service-plan" --resource-group "luiscasalas16-resource-group" --location "eastus2" --sku "F1"
```

### App service .Net

```cmd
::crear app service .Net
az webapp create --name "luiscasalas16-app-service-net" --resource-group "luiscasalas16-resource-group" --plan "luiscasalas16-app-service-plan" --runtime "dotnet:7"
```

### App service .Net Framework

```cmd
::crear app service .Net
az webapp create --name "luiscasalas16-app-service-netfw" --resource-group "luiscasalas16-resource-group" --plan "luiscasalas16-app-service-plan" --runtime "ASPNET:V4.8"
```
