# example-azure

Este repositorio contiene ejemplos y documentación relacionada con Azure.

- [Ejemplos y Guías](#ejemplos-y-guías)
- [Documentación](#documentación)
- [Artículos](#artículos)
- [Herramientas](#herramientas)
  - [Azure CLI](#azure-cli)
  - [Azure SDK](#azure-sdk)
  - [Azure Identity](#azure-identity)

---

## Ejemplos y Guías

Proyectos de ejemplo de servicios de Azure.

- [Active Directory](/active-directory/README.md)
- [Subscription](/subscription/README.md)
- [Resource Group](/resource-group/README.md)
- [Virtual Network](/virtual-network/README.md)
- [Application Service](/application-service/README.md)
- [Virtual Machine](/virtual-machine/README.md)
- [Key Vault](/key-vault/README.md)
- [Static Web App](/static-web-app/README.md)
- [Container Instances](/container-instances/README.md)
- [Container Registry](/container-registry/README.md)
- [Application Gateway](/application-gateway/README.md)

Guías prácticas.

- [Secretos](/how-to/secrets.md)
- [Autenticación](/how-to/authentication.md)

---

## Documentación

- [Application Architecture Fundamentals](https://learn.microsoft.com/en-us/azure/architecture/guide)
- [Azure Roles](https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles)
- [Azure Resources Providers](https://learn.microsoft.com/en-us/azure/role-based-access-control/resource-provider-operations)
- [AzAdvertizer](https://www.azadvertizer.net/index.html)

---

## Artículos

- [Application Architecture Guide](http://bit.ly/2BppIFo)
- [Application Architecture Maps Overview](https://medium.com/@kanchan.tewary/microsoft-azure-mind-maps-86bd6e442988)
- [Azure Proactive Resiliency Library](https://azure.github.io/Azure-Proactive-Resiliency-Library/services/)

---

## Herramientas

### Azure CLI

Es un conjunto de comandos que se utilizan para la administración de recursos de Azure. Están diseñados para trabajar rápidamente con Azure, con énfasis en la automatización.

- [Resumen](https://learn.microsoft.com/en-us/cli/azure/what-is-azure-cli)
- [Instalación](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Referencia](https://learn.microsoft.com/en-us/cli/azure/reference-index)

```powershell
#instalación
Invoke-WebRequest -Uri 'https://aka.ms/installazurecliwindowsx64' -OutFile '.\AzureClix64.msi';
Start-Process msiexec.exe -Wait -ArgumentList '/I AzureClix64.msi /quiet';
Remove-Item .\AzureClix64.msi;
```

```powershell
#autenticación
az login
```

```powershell
#versión
az version
```

```powershell
#actualización
az upgrade
```

```powershell
#obtener configuración
az config get
```

```powershell
#configuración actualización automática
az config set auto-upgrade.enable=yes
az config set auto-upgrade.prompt=no
#configuración instalación automática dependencias
az config set extension.use_dynamic_install=yes_without_prompt
#configuración formato de salida por defecto
az config set core.output=jsonc
```

### Azure PowerShell

Es un conjunto de comandos nativos de PowerShell que se utilizan para la administración de recursos de Azure. Están diseñados para trabajar rápidamente con Azure, con énfasis en la automatización.

- [Resumen](https://learn.microsoft.com/en-us/powershell/azure/what-is-azure-powershell)
- [Instalación](https://learn.microsoft.com/en-us/powershell/azure/install-azps-windows)
- [Referencia](https://learn.microsoft.com/en-us/powershell/module/?view=azps-10.2.0)

```powershell
#instalación
cmd (administrator)
powershell.exe -NoProfile
cd $env:temp
Invoke-WebRequest -Uri 'https://gist.githubusercontent.com/luiscasalas16/aaf1edbeb8d331384ad503c454a2e8e4/raw' -OutFile '.\CleanAzPs.ps1'
powershell.exe -ExecutionPolicy bypass -File '.\CleanAzPs.ps1'
exit

pwsh.exe
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
Install-Module -Name Az -Repository PSGallery -Force
exit
```

```powershell
#versión
Get-Module -ListAvailable -Name Az -Refresh
```

```powershell
#actualización
Update-Module -Name Az -Force
```

### Azure SDK

Facilitar el uso de los servicios de Azure desde .Net. Proporciona una interfaz uniforme y familiar para acceder a los servicios de Azure. Está disponible como una serie de paquetes NuGet que se pueden usar en aplicaciones .NET (5 y superior) y .NET Framework (4.7.2 y superior).

- [Resumen](https://learn.microsoft.com/en-us/dotnet/azure/sdk/azure-sdk-for-dotnet)
- [Autenticación](https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication)

### Azure Identity

Es una biblioteca que proporciona compatibilidad con la autenticación de tokens de Azure Active Directory en Azure SDK. Proporciona un conjunto de implementaciones de TokenCredential que se pueden usar para la autenticación de servicios de Azure.

- [Resumen](https://learn.microsoft.com/en-us/dotnet/api/overview/azure/identity-readme)
- [Documentación](https://azuresdkdocs.blob.core.windows.net/$web/dotnet/Azure.Identity/1.9.0/index.html)
