# example-azure / container-registry

[Azure Container Registry Documentation](https://learn.microsoft.com/en-us/azure/container-registry)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Container Registry.

```powershell
# crear container registry
az acr create --name "lcs16cr" --resource-group "lcs16-rg" --sku "Basic" --admin-enabled "true"
```

```powershell
# listar imagenes
az acr repository list --name "lcs16cr" --out table
# listar tags de imagen
az acr repository show-tags --name "lcs16cr" --repository "hello-world" --out table
```

```powershell
# publicar imagen
az acr login --name "lcs16cr"
docker pull mcr.microsoft.com/hello-world
docker tag mcr.microsoft.com/hello-world lcs16cr.azurecr.io/hello-world:v1
docker push lcs16cr.azurecr.io/hello-world:v1
docker rmi lcs16cr.azurecr.io/hello-world:v1
# eliminar imagen
az acr repository delete --name "lcs16cr" --image "hello-world:v1"
```
