# example-azure

Proyectos de ejemplo de Azure.

## azure-cli

- [Resumen](https://learn.microsoft.com/en-us/cli/azure/what-is-azure-cli)
- [Servicios](https://learn.microsoft.com/en-us/cli/azure/azure-services-the-azure-cli-can-manage)
- [Instalación](https://learn.microsoft.com/en-us/cli/azure/azure-services-the-azure-cli-can-manage)

```cmd
::actualización
    az upgrade

::configuración
    az config get
::actaulización automática
    az config set auto-upgrade.enable=yes
    az config set auto-upgrade.prompt=no
::instalación automática
    az config set extension.use_dynamic_install=yes_without_prompt
::formato de salida por defecto
    az config set core.output=jsonc

::autenticación
    az login

::suscripción
    az account show

::resource groups
    az group list
    az group create --name MyResourceGroup --location eastus2
    az group delete --name MyResourceGroup
```

## azure-sdk

- [Resumen](https://learn.microsoft.com/en-us/dotnet/azure/sdk/azure-sdk-for-dotnet)
- [Autenticación](https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication)
- [Identidades](https://learn.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview)

## azure-identity

- [Resumen](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/identity-readme)
- [Documentación](https://azuresdkdocs.blob.core.windows.net/$web/dotnet/Azure.Identity/1.8.0/index.html)

## prácticas

- [Application Architecture Fundamentals](https://learn.microsoft.com/en-us/azure/architecture/guide)
- [Application Architecture Guide](http://bit.ly/2BppIFo)
- [Application Architecture Maps Overview](https://medium.com/@kanchan.tewary/microsoft-azure-mind-maps-86bd6e442988)
