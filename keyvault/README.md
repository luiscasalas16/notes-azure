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

//crear grupo en azure ad
az ad group create --display-name "luiscasalas16-group-developers" --mail-nickname "luiscasalas16-group-developers" --description "developers groups"

//incluir aplicación en grupo
Azure Active Directory -> Groups -> Members -> New

//establecer permiso en grupo
luiscasalas16-key-vault -> Access policies -> New -> Permissions (Secret (Get,List)) -> Principal (luiscasalas16-group-developers)

```
