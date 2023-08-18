# example-azure / container-registry

[Azure Container Registry Documentation](https://learn.microsoft.com/en-us/azure/container-registry)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de un Container Registry.

```powershell
# crear container registry
az acr create --name "lcs16containerregistry" --resource-group "lcs16-rg" --sku Basic
# habilitar administrador en container registry
az acr update --name "lcs16containerregistry" --admin-enabled true
```

```powershell
# listar imagenes
az acr repository list --name "lcs16containerregistry" --out table
# listar tags de imagen
az acr repository show-tags --name "lcs16containerregistry" --repository "hello-world" --out table
```

```powershell
# publicar imagen
az acr login --name "lcs16containerregistry"
docker pull mcr.microsoft.com/hello-world
docker tag mcr.microsoft.com/hello-world lcs16containerregistry.azurecr.io/hello-world:v1
docker push lcs16containerregistry.azurecr.io/hello-world:v1
docker rmi lcs16containerregistry.azurecr.io/hello-world:v1
# eliminar imagen
az acr repository delete --name "lcs16containerregistry" --image "hello-world:v1"
```
