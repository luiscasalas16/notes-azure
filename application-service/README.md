# example-azure / application-service

[Azure App Service Documentation](https://learn.microsoft.com/en-us/azure/app-service)

- [Comandos Application Service Plan](#comandos-application-service-plan)
- [Comandos Application Service](#comandos-application-service)

---

## Documentación

- [Kudu](https://github.com/projectkudu/kudu/wiki)

---

## Comandos Application Service Plan

Comandos generales para la administración de un Application Service Plan.

```powershell
# crear app service plan
    # --is-linux = linux operating system
    # --number-of-workers = number of workers instances
    # --zone-redundant = high availability zone
az appservice plan create --name "lcs16-asp" --resource-group "lcs16-rg" --location "eastus" --sku "F1"
```

```powershell
# eliminar app service plan
az appservice plan delete --name "lcs16-asp" --resource-group "lcs16-rg"
```

---

## Comandos Application Service

Comandos generales para la administración de un Application Service.

```powershell
# crear app service .Net (dotnet:7 para windows o DOTNETCORE:7.0 para linux)
az webapp create --name "lcs16-as-net" --resource-group "lcs16-rg" --plan "lcs16-asp" --runtime "dotnet:7"
# crear app service .Net Framework
az webapp create --name "lcs16-as-netfw" --resource-group "lcs16-rg" --plan "lcs16-asp" --runtime "ASPNET:V4.8"
```

```powershell
# eliminar app service .Net
az webapp delete --name "lcs16-as-net" --resource-group "lcs16-rg" --keep-empty-plan
# eliminar app service .Net Framework
az webapp delete --name "lcs16-as-netfw" --resource-group "lcs16-rg" --keep-empty-plan
```

```powershell
# asignar system-assigned identity a app service
az webapp identity assign --name "lcs16-as-net" --resource-group "lcs16-rg"
az webapp identity assign --name "lcs16-as-netfw" --resource-group "lcs16-rg"
# eliminar system-assigned identity a app service
az webapp identity remove --name "lcs16-as-net" --resource-group "lcs16-rg"
az webapp identity remove --name "lcs16-as-netfw" --resource-group "lcs16-rg"
```

```powershell
# asignar user-assigned identity a app service
az webapp identity assign --name "lcs16-as-net" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
az webapp identity assign --name "lcs16-as-netfw" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
# eliminar user-assigned identity a app service
az webapp identity remove --name "lcs16-as-net" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
az webapp identity remove --name "lcs16-as-netfw" --resource-group "lcs16-rg" --identities "/subscriptions/8e8b8f6d-3e0b-45fd-aa1b-f7aa212317cb/resourcegroups/lcs16-rg/providers/Microsoft.ManagedIdentity/userAssignedIdentities/lcs16-managed-identity"
```

```powershell
# establecer appsettings a app service
az webapp config appsettings set --name "lcs16-as-net" --resource-group "lcs16-rg" --settings 'AZURE_CLIENT_ID=bd8b9733-8e70-499c-9357-8e8bdc2fe22c'
az webapp config appsettings set --name "lcs16-as-netfw" --resource-group "lcs16-rg" --settings 'AZURE_CLIENT_ID=bd8b9733-8e70-499c-9357-8e8bdc2fe22c'
```
