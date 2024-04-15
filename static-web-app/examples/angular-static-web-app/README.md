# angular-static-web-app

Los comandos para generarla son:

- ng new angular-static-web-app
  - version = 17.3.4
  - stylesheet = SCSS

Los comandos para ejecutarla son:

- npm install
- ng serve -o
- ng build

```powershell
# ANGULAR
# 1. crear static web app
az staticwebapp create --name "lcs16-swa-angular" --resource-group "lcs16-rg" --location "eastus2" --sku "Free" --source "https://github.com/luiscasalas16/notes-azure" --branch main --app-location "/static-web-app/examples/angular-static-web-app" --output-location "dist/angular-static-web-app/browser" --login-with-github
# 2. autenticar en github
# 3. esperar a github action
# 4. obtener url del static web app
az staticwebapp show --name "lcs16-swa-angular" --query "defaultHostname"
```

```powershell
# eliminar static web app
az staticwebapp delete --name "lcs16-swa-angular" --resource-group "lcs16-rg"
```
