# example-azure / key-vault

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Key Vault.

```powershell
# crear key vault rbac
az keyvault create --name "luiscasalas16-key-vault" --resource-group "luiscasalas16-resource-group" --location "eastus2" --enable-rbac-authorization "true"

# establecer a administrador rol "Key Vault Administrator" en key vault
az role assignment create --assignee "10c7f93b-8116-426f-a052-af5d7411e7e0" --role "00482a5a-887f-4fb3-b363-3b7fe8e74483" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/luiscasalas16-resource-group/providers/Microsoft.KeyVault/vaults/luiscasalas16-key-vault"

# establecer a identities rol "Key Vault Secrets User" en key vault
# (luiscasalas16-application)
az role assignment create --assignee "94060eda-d539-457f-9a0d-4753c74b63dc" --role "4633458b-17de-408a-b874-0445c86b69e6" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/luiscasalas16-resource-group/providers/Microsoft.KeyVault/vaults/luiscasalas16-key-vault"
# (luiscasalas16-managed-identity)
az role assignment create --assignee "faf80c92-b94b-4acf-86fe-2eaaa9323b51" --role "4633458b-17de-408a-b874-0445c86b69e6" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/luiscasalas16-resource-group/providers/Microsoft.KeyVault/vaults/luiscasalas16-key-vault"
```

```powershell
#crear secret en keyvault
az keyvault secret set --vault-name "luiscasalas16-key-vault" --name "SecretNameKeyVault" --value "secret value in key vault"
```
