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

    # construir imagen
    docker build --tag "net-static-web-app:latest" .

    # subir imagen
    az acr login --name "lcs16cr"
    docker tag "net-static-web-app:latest" "lcs16cr.azurecr.io/net-static-web-app:latest"
    docker push "lcs16cr.azurecr.io/net-static-web-app:latest"

# crear identity para conexión entre app service y container registry
az identity create --name "lcs16-ca-mi" --resource-group "lcs16-rg"

$identity_id=$(az identity show --name "lcs16-ca-mi" --resource-group "lcs16-rg" --query id -o tsv)
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

# eliminar container apps environment
az containerapp env delete --name "lcs16-ca-env" --resource-group "lcs16-rg"
```

```powershell
    # crear contenedor
    az containerapp create --name "lcs16-ca-api" --resource-group "lcs16-rg" --environment "lcs16-ca-env" --image "lcs16cr.azurecr.io/net-static-web-app:latest"  --user-assigned $identity_id --registry-identity $identity_id --registry-server "lcs16cr.azurecr.io" --ingress 'external' --target-port 8080 --query properties.configuration.ingress.fqdn

    # eliminar contenedor
    az containerapp delete --name "lcs16-ca-api" --resource-group "lcs16-rg"
```
