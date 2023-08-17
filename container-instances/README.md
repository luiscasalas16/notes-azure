# example-azure / container-instances

[Azure Container Instances Documentation](https://learn.microsoft.com/en-us/azure/container-instances)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Container Instances.

```powershell
# listar contenedores
az container list --resource-group "lcs16-rg" --out table
```

```powershell
# crear contenedor
az container create --name "lcs16-ci-netdockerconsole" --resource-group "lcs16-rg" --image "lusalas16/welcome-to-docker:latest" --dns-name-label "lcs16-ci-netdockerconsole" --ports 3000
# reiniciar contenedor
az container restart --name "lcs16-ci-netdockerconsole" --resource-group "lcs16-rg"
# ver events y logs
az container attach --name "lcs16-ci-netdockerconsole" --resource-group "lcs16-rg"
```

```powershell
az container delete --name "lcs16-ci-netdockerconsole" --resource-group "lcs16-rg" --yes
```
