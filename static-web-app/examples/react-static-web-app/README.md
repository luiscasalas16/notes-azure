# react-static-web-app

Los comandos para generarla son:

- npm create vite@latest
  - name = react-static-web-app
  - framework = React
  - variant = TypeScript

Los comandos para ejecutarla son:

- npm install
- npm run dev
- npm run build

```powershell
# REACT
# 1. crear static web app
az staticwebapp create --name "lcs16-swa-react" --resource-group "lcs16-rg" --location "eastus2" --sku "Free" --source "https://github.com/luiscasalas16/notes-azure" --branch main --app-location "/static-web-app/examples/react-static-web-app" --output-location "dist" --login-with-github
# 2. autenticar en github
# 3. esperar a github action
# 4. obtener url del static web app
az staticwebapp show --name "lcs16-swa-react" --query "defaultHostname"
```
