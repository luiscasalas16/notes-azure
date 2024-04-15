# angular-static-web-app

```powershell
# NET

# crear container registry
az acr create --name "lcs16cr" --resource-group "lcs16-rg" --sku Basic --admin-enabled true

# construir imagen
docker build --tag "net-static-web-app:latest" .

# subir imagen
az acr login --name "lcs16cr"
docker tag "net-static-web-app:latest" "lcs16cr.azurecr.io/net-static-web-app:latest"
docker push "lcs16cr.azurecr.io/net-static-web-app:latest"

# crear identity para conexión entre app service y container registry
az identity create --name "lcs16-swa-mi" --resource-group "lcs16-rg"

# crear app service plan
az appservice plan create --name "lcs16-swa-asp" --resource-group "lcs16-rg" --location "eastus" --sku "F1" --is-linux

# crear app service
az webapp create --name "lcs16-swa-as" --resource-group "lcs16-rg" --plan "lcs16-swa-asp" --deployment-container-image-name "nginx"

# asignar identity a app service
$identity_id=$(az identity show --name "lcs16-swa-mi" --resource-group "lcs16-rg" --query id -o tsv)
az webapp identity assign --name "lcs16-swa-as" --resource-group "lcs16-rg" --identities $identity_id

# configurar app service para utilizar identity para uso de container registry
$identity_client_id=$(az identity show --name "lcs16-swa-mi" --resource-group "lcs16-rg" --query clientId -o tsv)
$webapp_config=$(az webapp show --name "lcs16-swa-as" --resource-group "lcs16-rg" --query id --output tsv)+"/config/web"
az resource update --ids $webapp_config --set properties.acrUseManagedIdentityCreds=True
az resource update --ids $webapp_config --set properties.AcrUserManagedIdentityID=$identity_client_id

# configurar identity para uso de container registry
$identity_principal_id=$(az identity show --resource-group "lcs16-rg" --name "lcs16-swa-mi" --query principalId --output tsv)
$container_registry_id=$(az acr show --resource-group "lcs16-rg" --name "lcs16cr" --query id --output tsv)
az role assignment create --assignee $identity_principal_id --scope $container_registry_id --role "AcrPull"

$acr=$(az acr show --name "lcs16cr" --resource-group "lcs16-rg" --query loginServer --output tsv)
$image="net-static-web-app:latest"
$fxVersion="Docker|"+$acr+"/"+$Image

az resource update --ids $webapp_config --set '"properties.linuxFxVersion=Docker|lcs16cr.azurecr.io/net-static-web-app:latest"'


az webapp config appsettings set --name "lcs16-swa-as" --resource-group "lcs16-rg" --settings WEBSITES_PORT=8080
```
