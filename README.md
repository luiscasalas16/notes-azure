# example-azure

Proyectos de ejemplo de Azure.

## Azure CLI

Es un conjunto de comandos que se utilizan para la administración de recursos de Azure. Está diseñada para trabajar rápidamente con Azure, con énfasis en la automatización.

- [Resumen](https://learn.microsoft.com/en-us/cli/azure/what-is-azure-cli)
- [Instalación](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Referencia](https://learn.microsoft.com/en-us/cli/azure/reference-index)

```powershell
#actualización
    az upgrade

#configuración
    az config get
#actaulización automática
    az config set auto-upgrade.enable=yes
    az config set auto-upgrade.prompt=no
#instalación automática
    az config set extension.use_dynamic_install=yes_without_prompt
#formato de salida por defecto
    az config set core.output=jsonc

#autenticación
    az login

#suscripción
    az account show
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
