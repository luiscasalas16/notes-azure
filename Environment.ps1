##### grupo de recursos

# crear grupo de recursos
az group create --name "lcs16-rg" --location "eastus"

##### identidades

# crear application identity
az ad sp create-for-rbac --name "lcs16-application-identity"
# crear managed identity
az identity create --name "lcs16-managed-identity" --resource-group "lcs16-rg"

##### key vault

# crear key vault
az keyvault create --name "lcs16-kv" --resource-group "lcs16-rg" --location "eastus" --enable-rbac-authorization "true"
# establecer a administrador rol "Key Vault Administrator" en key vault
az role assignment create --assignee "10c7f93b-8116-426f-a052-af5d7411e7e0" --role "00482a5a-887f-4fb3-b363-3b7fe8e74483" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.KeyVault/vaults/lcs16-kv"
# buscar identidades
az ad sp list --all --query "[?contains(displayName,'lcs16')].{ Id:id, DisplayName:displayName }" --out table
# establecer a identities rol "Key Vault Secrets User" en key vault
# (lcs16-application-identity)
az role assignment create --assignee "0988b0c8-cb2e-4537-b4db-4878adfd4d98" --role "4633458b-17de-408a-b874-0445c86b69e6" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.KeyVault/vaults/lcs16-kv"
# (lcs16-managed-identity)
az role assignment create --assignee "ed9cbe76-ebcc-4420-817c-425d5d97045e" --role "4633458b-17de-408a-b874-0445c86b69e6" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.KeyVault/vaults/lcs16-kv"
#crear secret en keyvault
az keyvault secret set --vault-name "lcs16-kv" --name "SecretNameKeyVault" --value "secret value in key vault"

##### application service

# crear app service plan
az appservice plan create --name "lcs16-asp" --resource-group "lcs16-rg" --location "eastus" --sku "F1"
# crear app service .Net
az webapp create --name "lcs16-as-net" --resource-group "lcs16-rg" --plan "lcs16-asp" --runtime "dotnet:7"
# crear app service .Net Framework
az webapp create --name "lcs16-as-netfw" --resource-group "lcs16-rg" --plan "lcs16-asp" --runtime "ASPNET:V4.8"
# asignar user-assigned identity a app service
az webapp identity assign --name "lcs16-as-net" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
az webapp identity assign --name "lcs16-as-netfw" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
# establecer user-assigned identity client id en appsettings a app service
az webapp config appsettings set --name "lcs16-as-net" --resource-group "lcs16-rg" --settings 'AZURE_CLIENT_ID=69c220ea-f2f1-4c5a-a324-b7523c94118c'
az webapp config appsettings set --name "lcs16-as-netfw" --resource-group "lcs16-rg" --settings 'AZURE_CLIENT_ID=69c220ea-f2f1-4c5a-a324-b7523c94118c'
# publicar aplicación en app service
az webapp deployment source config-zip --name "lcs16-as-net" --resource-group "lcs16-rg" --src ".\_dist\NetKeyVaultWebMvc.zip"
az webapp deployment source config-zip --name "lcs16-as-netfw" --resource-group "lcs16-rg" --src ".\_dist\NetFwKeyVaultWebMvc.zip"
