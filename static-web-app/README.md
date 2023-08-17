# example-azure / static-web-app

[Azure Static Web Apps Documentation](https://learn.microsoft.com/en-us/azure/static-web-apps)

- [Comandos](#comandos)

---

## Comandos

Comandos generales para la administración de una Static Web App.

```powershell
# REACT
# 1. crear static web app
az staticwebapp create --name "lcs16-swa-react" --resource-group "lcs16-rg" --location "eastus2" --sku "Free" --source "https://github.com/luiscasalas16/test-azure-static-web-app-react" --branch main --app-location "/" --output-location "dist" --login-with-github
# 2. autenticar en github
# 3. esperar a github action
# 4. obtener url del static web app
az staticwebapp show --name "lcs16-swa-react" --query "defaultHostname"
```

```powershell
# REACT
# 1. crear static web app
az staticwebapp create --name "lcs16-swa-angular" --resource-group "lcs16-rg" --location "eastus2" --sku "Free" --source "https://github.com/luiscasalas16/test-azure-static-web-app-angular" --branch main --app-location "/" --output-location "dist/test-azure-static-web-app-angular" --login-with-github
# 2. autenticar en github
# 3. esperar a github action
# 4. obtener url del static web app
az staticwebapp show --name "lcs16-swa-angular" --query "defaultHostname"
```

```powershell
# eliminar static web app
az staticwebapp delete --name "lcs16-swa-react" --resource-group "lcs16-rg" --yes
az staticwebapp delete --name "lcs16-swa-angular" --resource-group "lcs16-rg" --yes
```
