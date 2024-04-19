# example-azure / database-mysql

[Azure MySql Database Documentation](hhttps://learn.microsoft.com/en-us/azure/mysql)

- [Comandos Sql Database Server](#comandos-sql-database-server)
- [Comandos Sql Database](#comandos-sql-database)

---

## Comandos Sql Database Server

```powershell
# crear mysql database server
az mysql flexible-server create --resource-group "lcs16-rg" --name "lcs16-mysqs" --admin-user "lcs16mysqsuser" --admin-password "6C2n0o7cNMYvcD0ZxJzvr1qiH" --sku-name "Standard_B1ms" --tier "Burstable" --public-access "None" --storage-size 32 --version 8.0.21 --high-availability "Disabled" --storage-auto-grow "Disabled" --auto-scale-iops "Disabled"
```

```powershell
# configurar firewall mysql database server
az mysql flexible-server firewall-rule create --resource-group "lcs16-rg" --name "lcs16-mysqs" --rule-name "AllIpAddress" --start-ip-address 0.0.0.0 --end-ip-address 255.255.255.255
```

```powershell
# eliminar mysql database server
az mysql flexible-server delete --resource-group "lcs16-rg" --name "lcs16-mysqs"
```

## Comandos Sql Database

```powershell
# crear mysql database
az mysql flexible-server db create --resource-group "lcs16-rg" --server-name "lcs16-mysqs"  --database-name "lcs16-mysq"
```

```powershell
# eliminar mysql database
az mysql flexible-server db delete --resource-group "lcs16-rg" --server-name "lcs16-mysqs"  --database-name "lcs16-mysq"
```

## Conexión

### MySQL Shell

- [Descarga](https://dev.mysql.com/downloads/shell)
- [Documentación](https://dev.mysql.com/doc/mysql-shell/8.0/en/mysqlsh.html)

```powershell
# terminal
mysqlsh --host "lcs16-mysqs.mysql.database.azure.com" --user="lcs16mysqsuser" --password="6C2n0o7cNMYvcD0ZxJzvr1qiH" --database "lcs16-mysq" --sql
# ejecutar comando
mysqlsh --host "lcs16-mysqs.mysql.database.azure.com" --user="lcs16mysqsuser" --password="6C2n0o7cNMYvcD0ZxJzvr1qiH" --database "lcs16-mysq" --sql --execute "show databases;"
# ejecutar script
mysqlsh --host "lcs16-mysqs.mysql.database.azure.com" --user="lcs16mysqsuser" --password="6C2n0o7cNMYvcD0ZxJzvr1qiH" --database "lcs16-mysq" --sql --file "C:\..."
```
