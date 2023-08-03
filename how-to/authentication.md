# example-azure / how-to / authentication

Cómo realizar la autenticación de aplicaciones .Net en Azure.

- [1. Autenticación para desarrollo](#1-autenticación-para-desarrollo)
  - [1.1 Por cuenta de usuario de azure](#11-por-cuenta-de-usuario-de-azure)
  - [1.2 Por service principal de azure](#12-por-service-principal-de-azure)
- [## 2. Autenticación para producción](#2-autenticación-para-producción)
  - [2.1. En tierra por service principals de azure](#21-en-tierra-por-service-principals-de-azure)
  - [2.2. En nube por managed identity de azure](#22-en-nube-por-managed-identity-de-azure)
    - [2.2.1 System-assigned managed identity](#221-system-assigned-managed-identity)
    - [2.2.2 User-assigned managed identity](#222-user-assigned-managed-identity)

[How to authenticate .NET applications using Azure SDK](https://learn.microsoft.com/en-us/dotnet/azure/sdk/authentication)

---

## 1. Autenticación para desarrollo

### 1.1 Por cuenta de usuario de azure

- La identidad se obtiene de la herramienta de desarrollo o de la linea de comandos.
- Se utiliza la misma cuenta de usuario del desarrollador en azure.
- Se tienen más permisos de los requeridos para el desarrollo, por lo que es un potencial problema en producción.
- Se utiliza DefaultAzureCredential.
  - ```csharp
    TokenCredential credential = new DefaultAzureCredential();
    ```

```powershell
#no requiere configuración, ya que se utilizan los mismos permisos de la cuenta de usuario de azure.
```

### 1.2 Por service principal de azure

- La identidad se obtiene por el registro de un service principal y el uso del TENANT_ID, CLIENT_ID, CLIENT_SECRET.
- Se requiere la configuración del service principal en azure.
- Se tienen los mismos permisos para desarrollo y para producción.
- Se recomienda utilizar un service principal diferente para cada desarrollador y aplicación.
- Se utiliza:
  - DefaultAzureCredential con variables de ambiente (AZURE_TENANT_ID, AZURE_CLIENT_ID y AZURE_CLIENT_SECRET) establecidas en "launch settings.json -> environmentVariables".
    - ```csharp
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
  - ClientSecretCredential con parámetros por programación.
    - ```csharp
      TokenCredential credential = new ClientSecretCredential ("AZURE_TENANT_ID", "AZURE_CLIENT_ID", "AZURE_CLIENT_SECRET");
      ```

```powershell
#crear service principal para desarrollador y aplicación en azure ad
az ad sp create-for-rbac --name "luiscasalas16-application-developer"

#incluir service principal del desarrollador y aplicación en grupo en azure ad
Azure Active Directory -> Groups -> Members -> Add
```

## 2. Autenticación para producción

### 2.1. En tierra por service principals de azure

- La identidad se obtiene por el registro de una aplicación y el uso del TENANT_ID, CLIENT_ID, CLIENT_SECRET.
- Se requiere la configuración de la aplicación en azure.
- Se recomienda utilizar un service principal diferente para cada ambiente.
- Se utiliza:
  - DefaultAzureCredential con variables de ambiente (AZURE_TENANT_ID, AZURE_CLIENT_ID y AZURE_CLIENT_SECRET) establecidas en "web.config" -> "system.webServer" -> "aspNetCore" -> "environmentVariables"
    - ```csharp
      TokenCredential credential = new DefaultAzureCredential();
      ```
  - ClientSecretCredential con parámetros por programación.
    - ```csharp
      TokenCredential credential = new ClientSecretCredential ("AZURE_TENANT_ID", "AZURE_CLIENT_ID", "AZURE_CLIENT_SECRET");
      ```

```powershell
#crear service principal para desarrollador y aplicación en azure ad
az ad sp create-for-rbac --name "luiscasalas16-application-production"

#incluir service principal del desarrollador y aplicación en grupo en azure ad
Azure Active Directory -> Groups -> Members -> Add
```

### 2.2. En nube por managed identity de azure

```powershell
#crear app service plan
az appservice plan create --name "luiscasalas16-app-service-plan" --resource-group "luiscasalas16-resource-group" --location "eastus2" --sku "F1"

#crear app service
az webapp create --name "luiscasalas16-app-service-web" --resource-group "luiscasalas16-resource-group" --plan "luiscasalas16-app-service-plan" --runtime "dotnet:7"
```

#### 2.2.1 System-assigned managed identity

- Se crea como parte de un recurso.
- Comparte el cliclo de vida del recurso.
- No se puede compartir.
- Se utiliza DefaultAzureCredential.
  - ```csharp
    TokenCredential credential = new DefaultAzureCredential();
    ```

```powershell
#habilitar la system managed identity
az webapp identity assign --name "luiscasalas16-app-service-web" --resource-group "luiscasalas16-resource-group"

#incluir system managed principal en grupo en azure ad
Azure Active Directory -> Groups -> Members -> Add
```

#### 2.2.2 User-assigned managed identity

- Se crea como un recurso independiente.
- Tiene un cliclo de vida independiente.
- Se puede compartir, la misma identidad puede ser utilizada en múltiples recursos.
- Se utiliza:
  - DefaultAzureCredential con variables de ambiente (AZURE_CLIENT_ID) establecidas en "Configuration" -> "Application Settings".
    - ```csharp
      TokenCredential credential = new DefaultAzureCredential();
      ```
  - ManagedIdentityCredential con parámetros por programación.
    - ```csharp
      TokenCredential credential = new ManagedIdentityCredential("AZURE_CLIENT_ID");
      ```

```powershell
#crear la user managed identity
az identity create --name "luiscasalas16-user-identity" --resource-group "luiscasalas16-resource-group"

#incluir user managed principal en grupo en azure ad
Azure Active Directory -> Groups -> Members -> Add

#asociar la user managed identity
luiscasalas16-app-service-web -> Identity -> User assigned -> Add -> luiscasalas16-user-identity
```
