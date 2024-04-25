# example-azure / container-apps

[Azure Container Apps Documentation](https://learn.microsoft.com/en-us/azure/container-apps)

Comandos generales para la administración de un Container Apps.

- [Comandos](#comandos)

---

```powershell

```

## Comandos Container Registry

```powershell
# crear container registry
az acr create --name "lcs16cr" --resource-group "lcs16-rg" --sku "Basic" --admin-enabled "true"

$container_registry_id=$(az acr show --resource-group "lcs16-rg" --name "lcs16cr" --query id --output tsv)

# crear identity para conexión entre app service y container registry
az identity create --name "lcs16-ca-mi" --resource-group "lcs16-rg"

$identity_principal_id=$(az identity show --resource-group "lcs16-rg" --name "lcs16-ca-mi" --query principalId --output tsv)

# configurar identity para uso de container registry
az role assignment create --assignee $identity_principal_id --scope $container_registry_id --role "AcrPull"
```

```powershell
# construir y subir imágenes
az acr login --name "lcs16cr"

docker build --tag "lcs16cr.azurecr.io/net-container-apps-web-api:latest" --file "./container-apps/NetContainerAppsWebApi/Dockerfile" "./container-apps"
docker push "lcs16cr.azurecr.io/net-container-apps-web-api:latest"

docker build --tag "lcs16cr.azurecr.io/net-container-apps-web-mvc:latest" --file "./container-apps/NetContainerAppsWebMvc/Dockerfile" "./container-apps"
docker push "lcs16cr.azurecr.io/net-container-apps-web-mvc:latest"
```

## Comandos Container App Environment

```powershell
# crear container apps environment por consumo
az containerapp env create --name "lcs16-ca-env" --resource-group "lcs16-rg" --logs-destination "none" --enable-workload-profiles "false"
```

```powershell
# crear container apps environment por workload-profile
az containerapp env workload-profile list-supported --location "eastus"
az containerapp env create --name "lcs16-ca-env" --resource-group "lcs16-rg" --logs-destination "none" --enable-workload-profiles "true"
az containerapp env workload-profile add --name "lcs16-ca-wp" --resource-group "lcs16-rg" --workload-profile-name "lcs16-ca-env" --workload-profile-type "D4" --min-nodes 1 --max-nodes 1
```

```powershell
# eliminar container apps environment
az containerapp env delete --name "lcs16-ca-env" --resource-group "lcs16-rg" --yes
```

## Comandos Container App

```powershell
$identity_id=$(az identity show --name "lcs16-ca-mi" --resource-group "lcs16-rg" --query id -o tsv)

# crear contenedor ingress internal
az containerapp create --name "lcs16-ca-web-api" --resource-group "lcs16-rg" --environment "lcs16-ca-env" --image "lcs16cr.azurecr.io/net-container-apps-web-api:latest" --user-assigned $identity_id --registry-identity $identity_id --registry-server "lcs16cr.azurecr.io" --ingress "internal" --target-port 8080 --query properties.configuration.ingress.fqdn

$fqdn_api=$(az containerapp show --name "lcs16-ca-web-api" --resource-group "lcs16-rg" --query properties.configuration.ingress.fqdn)

# crear contenedor ingress external
az containerapp create --name "lcs16-ca-web-mvc" --resource-group "lcs16-rg" --environment "lcs16-ca-env" --image "lcs16cr.azurecr.io/net-container-apps-web-mvc:latest" --user-assigned $identity_id --registry-identity $identity_id --registry-server "lcs16cr.azurecr.io" --ingress "external" --target-port 8080 --env-vars Api="http://$fqdn_api" --query properties.configuration.ingress.fqdn
```

```powershell
# reiniciar contenedor
Stop-AzContainerApp -Name "lcs16-ca-web-mvc" -ResourceGroupName "lcs16-rg"
Start-AzContainerApp -Name "lcs16-ca-web-mvc" -ResourceGroupName "lcs16-rg"
```

```powershell
# log de system de contenedor
$containerAppName="lcs16-ca-web-mvc"
$resourceGroup="lcs16-rg"
az containerapp logs show --name $containerAppName --resource-group $resourceGroup  --type "system" --follow

# log de console de contenedor
$containerAppName="lcs16-ca-web-mvc"
$resourceGroup="lcs16-rg"
$version = $(az containerapp revision list --name $containerAppName --resource-group $resourceGroup --query "[0].name")
$replica = $(az containerapp replica list --name $containerAppName --resource-group $resourceGroup --revision $version --query "[0].name")
az containerapp logs show --name $containerAppName --resource-group $resourceGroup --revision $version --replica $replica --type "console" --follow
```

```powershell
# eliminar contenedores
az containerapp delete --name "lcs16-ca-web-api" --resource-group "lcs16-rg" --yes
az containerapp delete --name "lcs16-ca-web-mvc" --resource-group "lcs16-rg" --yes
```
