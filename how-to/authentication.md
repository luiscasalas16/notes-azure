# example-azure / how-to / authentication

Cómo realizar la autenticación de aplicaciones .Net en Azure.

- [1. Autenticación para desarrollo](#1-autenticación-para-desarrollo)
  - [1.1 Por cuenta de azure](#11-por-cuenta-de-azure)
  - [1.2 Por service principal de azure](#12-por-service-principal-de-azure)
- [2. Autenticación para producción](#2-autenticación-para-producción)
  - [2.1. En tierra por service principals de azure](#21-en-tierra-por-service-principals-de-azure)
  - [2.2. En nube por managed identity de azure](#22-en-nube-por-managed-identity-de-azure)
    - [2.2.1 System-assigned managed identity](#221-system-assigned-managed-identity)
      - [2.2.1.1 En application services](#2211-en-application-services)
      - [2.2.1.2 En virtual machine](#2212-en-virtual-machine)
    - [2.2.2 User-assigned managed identity](#222-user-assigned-managed-identity)
      - [2.2.2.1 En application services](#2221-en-application-services)
      - [2.2.2.2 En virtual machine](#2222-en-virtual-machine)

Referencias:

- [How to authenticate .NET applications using Azure SDK](https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication)

---

## 1. Autenticación para desarrollo

### 1.1 Por cuenta de azure

- La identidad se obtiene de la herramienta de desarrollo (VS o VSCODE) o de la linea de comandos (Azure CLI o Azure PowerShell).
- Se utiliza la misma cuenta del desarrollador en azure.
- Se tienen más permisos de los requeridos para el desarrollo, por lo que es un potencial problema en producción.
- Se recomienda utilizar un grupo y otorgarle a este los permisos. Luego incluir en el grupo las cuentas de cada desarrollador.

### 1.2 Por service principal de azure

- La identidad se obtiene por el registro de un service principal y el uso del TENANT_ID, CLIENT_ID, CLIENT_SECRET.
- Se requiere la configuración del service principal en azure.
- Se tienen los mismos permisos para desarrollo y para producción.
- Se recomienda utilizar un service principal diferente para cada desarrollador y para cada aplicación.
- Se recomienda utilizar un grupo para cada aplicación y otorgarle a este los permisos. Luego incluir en el grupo los service principal de cada desarrollador.
- Se puede utilizar la siguiente configuración del DefaultAzureCredential para evitar el uso de la cuenta del desarrollador y obligar la lectura del service principal.

```csharp
TokenCredential credential = new DefaultAzureCredential
(
  new DefaultAzureCredentialOptions()
  {
    ExcludeVisualStudioCredential = true,
    ExcludeVisualStudioCodeCredential = true,
    ExcludeAzureCliCredential = true,
    ExcludeAzurePowerShellCredential = true
  }
);
```

- Se debe establecer el TENANT_ID, CLIENT_ID, CLIENT_SECRET con el que se va a autenticar en variables de ambiente.

  - En .Net se pueden establecen las variables de ambiente en "launch settings.json -> environmentVariables".

    ```json
    {
      "profiles": {
        "console": {
          "commandName": "Project",
          "environmentVariables": {
            "DOTNET_ENVIRONMENT": "Development",
            "AZURE_TENANT_ID": "00000000-0000-0000-0000-000000000000",
            "AZURE_CLIENT_ID": "00000000-0000-0000-0000-000000000000",
            "AZURE_CLIENT_SECRET": "abcdefghijklmnopqrstuvwxyz"
          }
        }
      }
    }
    ```

  - En .Net y .Net Framework se pueden establecen las variables de ambiente a nivel del usuario por PowerShell. Si hay un cambio en las variables de ambiente se debe reiniciar el Visual Studio para que el cambio se aplique.

    ```powershell
    # registrar variables de ambiente a nivel del usuario
    [Environment]::SetEnvironmentVariable("AZURE_TENANT_ID", "00000000-0000-0000-0000-000000000000", "User")
    [Environment]::SetEnvironmentVariable("AZURE_CLIENT_ID", "00000000-0000-0000-0000-000000000000", "User")
    [Environment]::SetEnvironmentVariable("AZURE_CLIENT_SECRET", "abcdefghijklmnopqrstuvwxyz", "User")
    ```

## 2. Autenticación para producción

### 2.1. En tierra por service principals de azure

- La identidad se obtiene por el registro de una aplicación y el uso del TENANT_ID, CLIENT_ID, CLIENT_SECRET.
- Se requiere la configuración de la aplicación en azure.
- Se recomienda utilizar un service principal diferente para cada ambiente.
- Se debe establecer el TENANT_ID, CLIENT_ID, CLIENT_SECRET con el que se va a autenticar en variables de ambiente.

  - Se pueden establecer las variables de ambiente a nivel de sistema por PowerShell. Si hay un cambio en las variables de ambiente se deben reiniciar las aplicaciones para que el cambio se aplique.

    ```powershell
    # registrar variables de ambiente a nivel de la máquina
    [Environment]::SetEnvironmentVariable("AZURE_TENANT_ID", "00000000-0000-0000-0000-000000000000", "Machine")
    [Environment]::SetEnvironmentVariable("AZURE_CLIENT_ID", "00000000-0000-0000-0000-000000000000", "Machine")
    [Environment]::SetEnvironmentVariable("AZURE_CLIENT_SECRET", "abcdefghijklmnopqrstuvwxyz", "Machine")
    ```

  - Se pueden establecer las variables de ambiente a nivel del application pool en el IIS en el archivo C:\Windows\System32\inetsrv\config\applicationHost.config.

    ```powershell
    # registrar variables de ambiente a nivel del application pool
    "%systemroot%\system32\inetsrv\appcmd.exe" set config -section:system.applicationHost/applicationPools
        /+"[name='.NET v4.5'].environmentVariables.[name='AZURE_TENANT_ID',value='00000000-0000-0000-0000-000000000000']"
        /commit:apphost

    "%systemroot%\system32\inetsrv\appcmd.exe" set config -section:system.applicationHost/applicationPools
        /+"[name='.NET v4.5'].environmentVariables.[name='AZURE_CLIENT_ID',value='00000000-0000-0000-0000-000000000000']"
        /commit:apphost

    "%systemroot%\system32\inetsrv\appcmd.exe" set config -section:system.applicationHost/applicationPools
        /+"[name='.NET v4.5'].environmentVariables.[name='AZURE_CLIENT_SECRET',value='abcdefghijklmnopqrstuvwxyz']"
        /commit:apphost
    ```

### 2.2. En nube por managed identity de azure

#### 2.2.1 System-assigned managed identity

- Se crea como parte de un recurso.
- Comparte el ciclo de vida del recurso.
- No se puede compartir.

##### 2.2.1.1 En application services

- Asignar system-assigned identity al application service, que retornar un principalId.
- Otorgar permisos correspondientes al principalId.

```powershell
# asignar system-assigned identity a app service
az webapp identity assign --name "test-app-service" --resource-group "test-resource-group"
```

##### 2.2.1.2 En virtual machine

- Asignar system-assigned identity a la virtual machine, que retornar un systemAssignedIdentity.
- Otorgar permisos correspondientes al systemAssignedIdentity.

```powershell
# asignar system-assigned identity a virtual machine
az vm identity assign --name "test-virtual-machine" --resource-group "test-resource-group"
```

#### 2.2.2 User-assigned managed identity

- Se crea como un recurso independiente.
- Tiene un ciclo de vida independiente.
- Se puede compartir, la misma identidad puede ser utilizada en múltiples recursos.

##### 2.2.2.1 En application services

- Asignar user-assigned identity al application service.
- Otorgar permisos correspondientes al user-assigned identity.

  ```powershell
  # asignar user-assigned identity a app service
  az webapp identity assign --name "test-app-service" --resource-group "test-resource-group" --identities "/subscriptions/00000000-0000-0000-0000-000000000000/resourcegroups/test-resource-group/providers/Microsoft.ManagedIdentity/userAssignedIdentities/test-managed-identity"
  ```

- Se debe establecer el AZURE_CLIENT_ID con el que se va a autenticar en "Configuration" -> "Application Settings" con el "Client ID" del managed identity.

  ```powershell
  # establecer appsettings
  az webapp config appsettings set --name "lcs16-as" --resource-group "lcs16-rg"
    --settings 'AZURE_CLIENT_ID=00000000-0000-0000-0000-000000000000'
  ```

##### 2.2.2.2 En virtual machine

- Asignar user-assigned identity a la virtual machine.
- Otorgar permisos correspondientes al user-assigned identity.

  ```powershell
  # asignar user-assigned identity a virtual machine
  az vm identity assign --name "test-virtual-machine" --resource-group "test-resource-group" --identities "/subscriptions/00000000-0000-0000-0000-000000000000/resourcegroups/test-resource-group/providers/Microsoft.ManagedIdentity/userAssignedIdentities/test-managed-identity"
  ```

- Se debe establecer elAZURE_CLIENT_ID con el que se va a autenticar en una variable de ambiente.

  - Se pueden establecer las variables de ambiente a nivel de sistema por PowerShell. Si hay un cambio en las variables de ambiente se deben reiniciar las aplicaciones para que el cambio se aplique.

    ```powershell
    # registrar variable de ambiente a nivel de la máquina
    [Environment]::SetEnvironmentVariable("AZURE_CLIENT_ID", "00000000-0000-0000-0000-000000000000", "Machine")
    ```

  - Se pueden establecer las variables de ambiente a nivel del application pool en el IIS en el archivo C:\Windows\System32\inetsrv\config\applicationHost.config.

    ```powershell
    # registrar variable de ambiente a nivel del application pool
    "%systemroot%\system32\inetsrv\appcmd.exe" set config -section:system.applicationHost/applicationPools
        /+"[name='.NET v4.5'].environmentVariables.[name='AZURE_CLIENT_ID',value='00000000-0000-0000-0000-000000000000']"
        /commit:apphost
    ```
