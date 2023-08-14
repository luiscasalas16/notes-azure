# example-azure / container-instances

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Container Instances.

```powershell
# listar contenedores
az container list --resource-group "lcs16-rg" --output table
```

```powershell
# crear contenedor
az container create --name "lcs16-container-helloworld" --resource-group "lcs16-rg" --image "mcr.microsoft.com/azuredocs/aci-helloworld" --dns-name-label "lcs16-container-helloworld" --ports 80
```

```powershell
# status ProvisioningState
az container show --name "lcs16-container-helloworld" --resource-group "lcs16-rg" --query "{FQDN:ipAddress.fqdn,ProvisioningState:provisioningState}" --out table

# logs
az container logs --name "lcs16-container-helloworld" --resource-group "lcs16-rg"
```

```powershell
#

```

```powershell
#

```

```powershell
#

```
