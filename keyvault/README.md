# webjobs

Proyecto de ejemplo de Key Vault.

```
az login

//eliminar recursos
az group delete --name "luiscasalas16-resource-group"

//crear recursos
az group create --name "luiscasalas16-resource-group" --location "eastus2"
az keyvault create --name "luiscasalas16-key-vault" --resource-group "luiscasalas16-resource-group" --location "eastus2"

//crear parámetro en keyvault
az keyvault secret set --vault-name "luiscasalas16-key-vault" --name "SecretNameKeyVault" --value "secret_value_in_key_vault"

//crear parámetro en user secrets
dotnet user-secrets set "SecretNameUserSecrets" "secret_value_in_user_secrets"

//crear aplicación en azure ad
az ad sp create-for-rbac --name "luiscasalas16-application-developer-1"

/*
"appId": "c13ff45e-b0aa-495e-865d-87714cea7d39",
"displayName": "luiscasalas16-application-developer-1",
"password": "3ef8Q~2ILBT-Hn~6h-L3c01XwM85cDE~6c9-Pbyc",
"tenant": "c3db3a9b-847b-4bbe-b592-62ad5d4f6918"
*/

```
