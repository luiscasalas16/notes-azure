# example-azure

Proyectos de ejemplo de Azure.
-   webjobs: ejemplos de proyectos de web jobs.


# azure-cli

    [Resumen](https://learn.microsoft.com/en-us/cli/azure/what-is-azure-cli)
    [Servicios](https://learn.microsoft.com/en-us/cli/azure/azure-services-the-azure-cli-can-manage)
    [Instalación](https://learn.microsoft.com/en-us/cli/azure/azure-services-the-azure-cli-can-manage)

    actualización:
        az upgrade

    configuración:
        az config get
    actaulización automática:
        az config set auto-upgrade.enable=yes
        az config set auto-upgrade.prompt=no
    instalación automática:
        az config set extension.use_dynamic_install=yes_without_prompt
    formato de salida por defecto:
        az config set core.output=jsonc

    autenticación:
        az login
    
    suscripción:
        az account show

    resource groups
        az group list
        az group create --name MyResourceGroup --location eastus2
        az group delete --name MyResourceGroup