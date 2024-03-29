# example-azure / key-vault

[Azure Key Vault Documentation](https://learn.microsoft.com/en-us/azure/key-vault)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Key Vault.

```powershell
# crear key vault
az keyvault create --name "lcs16-kv" --resource-group "lcs16-rg" --location "eastus" --enable-rbac-authorization "true"

# buscar key vault
az keyvault list --query "[].{ Name:name, Id:id }" --out table

# establecer a administrador rol "Key Vault Administrator" en key vault
az role assignment create --assignee "10c7f93b-8116-426f-a052-af5d7411e7e0" --role "00482a5a-887f-4fb3-b363-3b7fe8e74483" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.KeyVault/vaults/lcs16-kv"

# establecer a identities rol "Key Vault Secrets User" en key vault
# (lcs16-application-identity)
az role assignment create --assignee "0988b0c8-cb2e-4537-b4db-4878adfd4d98" --role "4633458b-17de-408a-b874-0445c86b69e6" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.KeyVault/vaults/lcs16-kv"
# (lcs16-managed-identity)
az role assignment create --assignee "ed9cbe76-ebcc-4420-817c-425d5d97045e" --role "4633458b-17de-408a-b874-0445c86b69e6" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.KeyVault/vaults/lcs16-kv"
```

```powershell
#crear secret en keyvault
az keyvault secret set --vault-name "lcs16-kv" --name "SecretNameKeyVault" --value "secret value in key vault"
```
