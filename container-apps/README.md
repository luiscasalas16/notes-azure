# example-azure / container-apps

[Azure Container Apps Documentation](https://learn.microsoft.com/en-us/azure/container-apps)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Container Apps.

```powershell
# crear container registry
az acr create --name "lcs16cr" --resource-group "lcs16-rg" --sku "Basic" --admin-enabled "true"

$container_registry_id=$(az acr show --resource-group "lcs16-rg" --name "lcs16cr" --query id --output tsv)

# construir y subir imágenes
az acr login --name "lcs16cr"

docker build --tag "net-container-apps-web-api:latest" --file "./container-apps/NetContainerAppsWebApi/Dockerfile" "./container-apps"
docker tag "net-container-apps-web-api:latest" "lcs16cr.azurecr.io/net-container-apps-web-api:latest"
docker push "lcs16cr.azurecr.io/net-container-apps-web-api:latest"

docker build --tag "net-container-apps-web-mvc:latest" --file "./container-apps/NetContainerAppsWebMvc/Dockerfile" "./container-apps"
docker tag "net-container-apps-web-mvc:latest" "lcs16cr.azurecr.io/net-container-apps-web-mvc:latest"
docker push "lcs16cr.azurecr.io/net-container-apps-web-mvc:latest"

# crear identity para conexión entre app service y container registry
az identity create --name "lcs16-ca-mi" --resource-group "lcs16-rg"

$identity_principal_id=$(az identity show --resource-group "lcs16-rg" --name "lcs16-ca-mi" --query principalId --output tsv)

# configurar identity para uso de container registry
az role assignment create --assignee $identity_principal_id --scope $container_registry_id --role "AcrPull"
```

```powershell
# crear container apps environment por consumo
az containerapp env create --name "lcs16-ca-env" --resource-group "lcs16-rg" --logs-destination "none" --enable-workload-profiles "false"

# crear container apps environment por workload-profile
az containerapp env workload-profile list-supported --location "eastus"
az containerapp env create --name "lcs16-ca-env" --resource-group "lcs16-rg" --logs-destination "none" --enable-workload-profiles "true"
az containerapp env workload-profile add --name "lcs16-ca-wp" --resource-group "lcs16-rg" --workload-profile-name "lcs16-ca-env" --workload-profile-type "D4" --min-nodes 1 --max-nodes 1
```

```powershell
$identity_id=$(az identity show --name "lcs16-ca-mi" --resource-group "lcs16-rg" --query id -o tsv)

# crear contenedores
az containerapp create --name "lcs16-ca-web-api" --resource-group "lcs16-rg" --environment "lcs16-ca-env" --image "lcs16cr.azurecr.io/net-container-apps-web-api:latest" --user-assigned $identity_id --registry-identity $identity_id --registry-server "lcs16cr.azurecr.io" --ingress "internal" --target-port 8080 --query properties.configuration.ingress.fqdn

$fqdn_api=$(az containerapp show --name "lcs16-ca-web-api" --resource-group "lcs16-rg" --query properties.configuration.ingress.fqdn)

az containerapp create --name "lcs16-ca-web-mvc" --resource-group "lcs16-rg" --environment "lcs16-ca-env" --image "lcs16cr.azurecr.io/net-container-apps-web-mvc:latest" --user-assigned $identity_id --registry-identity $identity_id --registry-server "lcs16cr.azurecr.io" --ingress "external" --target-port 8080 --env-vars Api="http://$fqdn_api" --query properties.configuration.ingress.fqdn
```

```powershell
# eliminar contenedores
az containerapp delete --name "lcs16-ca-web-api" --resource-group "lcs16-rg" --yes
az containerapp delete --name "lcs16-ca-web-mvc" --resource-group "lcs16-rg" --yes

# eliminar container apps environment
az containerapp env delete --name "lcs16-ca-env" --resource-group "lcs16-rg" --yes
```
