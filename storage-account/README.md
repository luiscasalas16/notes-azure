# example-azure / storage-account

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Storage Account.

https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-cli

```powershell
# crear storage account
az storage account create --name "lcs16sa" --resource-group "lcs16-rg"  --location "eastus" --sku "Standard_LRS" --min-tls-version "TLS1_2" --allow-blob-public-access false --allow-cross-tenant-replication false

# buscar storage account
az storage account list --query "[].{ Name:name, Id:id }" --out table

# establecer a administrador rol Storage Account Contributor" en key vault
az role assignment create --assignee "10c7f93b-8116-426f-a052-af5d7411e7e0" --role "17d1049b-9a84-46fb-8f53-869881c3d3ab" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.Storage/storageAccounts/lcs16sa"

# (lcs16-application-identity)
# establecer a identities rol "Storage Blob Data Contributor" en storage account
az role assignment create --assignee "0988b0c8-cb2e-4537-b4db-4878adfd4d98" --role "ba92f5b4-2d11-453d-a403-e96b0029c9fe" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.Storage/storageAccounts/lcs16sa"
# establecer a identities rol "Storage Queue Data Contributor" en storage account
az role assignment create --assignee "0988b0c8-cb2e-4537-b4db-4878adfd4d98" --role "974c5e8b-45b9-4653-ba55-5f855dd0fb88" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.Storage/storageAccounts/lcs16sa"
# (lcs16-managed-identity)
# establecer a identities rol "Storage Blob Data Contributor" en storage account
az role assignment create --assignee "ed9cbe76-ebcc-4420-817c-425d5d97045e" --role "ba92f5b4-2d11-453d-a403-e96b0029c9fe" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.Storage/storageAccounts/lcs16sa"
# establecer a identities rol "Storage Queue Data Contributor" en storage account
az role assignment create --assignee "ed9cbe76-ebcc-4420-817c-425d5d97045e" --role "974c5e8b-45b9-4653-ba55-5f855dd0fb88" --scope "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourceGroups/lcs16-rg/providers/Microsoft.Storage/storageAccounts/lcs16sa"
```
