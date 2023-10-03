# example-azure / storage-account

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Storage Account.

https://learn.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-cli

```powershell
# crear storage account
az storage account create --name "lcs16sa" --resource-group "lcs16-rg"  --location "eastus" --sku "Standard_LRS" --min-tls-version "TLS1_2" --allow-blob-public-access false --allow-cross-tenant-replication false
```
