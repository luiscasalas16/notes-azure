# example-azure / sql-database

[Azure Sql Database Documentation](https://learn.microsoft.com/en-us/azure/azure-sql/database)

- [Comandos Sql Database Server](#comandos-sql-database-server)
- [Comandos Sql Database](#comandos-sql-database)

---

## Comandos Sql Database Server

Comandos generales para la administración de un Sql Database.

```powershell
# crear sql database server
az sql server create --resource-group "lcs16-rg" --name "lcs16-sqs" --location "eastus" --admin-user "lcs16-sqs-user" --admin-password "wExz@Gm,YYU%.0RK?+U6b1FAy"
```

```powershell
# configurar firewall sql database server
az sql server firewall-rule create --resource-group "lcs16-rg" --server "lcs16-sqs" --name "AllIpAddress" --start-ip-address 0.0.0.0 --end-ip-address 255.255.255.255
```

```powershell
# eliminar sql database server
az sql server delete --resource-group "lcs16-rg" --name "lcs16-sqs"
```

## Comandos Sql Database

```powershell
# crear sql database
az sql db create --resource-group "lcs16-rg" --server "lcs16-sqs" --name "lcs16-sq" --edition GeneralPurpose --family Gen5 --capacity 1 --compute-model Serverless --max-size 8GB --auto-pause-delay 60
```

```powershell
# eliminar sql database
az sql db delete --resource-group "lcs16-rg" --server "lcs16-sqs" --name "lcs16-sq"
```
