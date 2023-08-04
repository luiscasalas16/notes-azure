# example-azure / how-to / secrets

Cómo realizar el manejo de secretos en aplicaciones .Net, utilizando User Secrets en desarrollo y Key Vault Secrets en Azure.

- [.Net](#1-net)
  - [1.1 User Secrets](#11-user-secrets)
  - [1.2 Key Vault Secrets](#12-key-vault-secrets)
- [.Net Framework](#2-net-framework)
  - [2.1 User Secrets](#21-user-secrets)
  - [2.2 Key Vault Secrets](#22-key-vault-secrets)

---

## 1 .Net

- Las propiedades se combinan con la siguiente prioridad: 1. keyvault, 2. user secret, 3. app settings.

### 1.1 User Secrets

- Referenciar el paquete "Microsoft.Extensions.Configuration.UserSecrets".
- Ajustar el UserSecretsId en el archivo del proyecto si se desea utilizar un ID personalizado y no un GUID.
- Los secretos se almacena en formato JSON en la ruta "%APPDATA%\Microsoft\UserSecrets\<ID>\secrets.json".
- Ejecutar el método "AddUserSecrets" para incluir en el "ConfigurationBuilder" los secretos.

### 1.2 Key Vault Secrets

- Referenciar los paquetes
  - "Azure.Extensions.AspNetCore.Configuration.Secrets"
  - "Azure.Identity"
- Ejecutar el método "AddAzureKeyVault" para incluir en el "ConfigurationBuilder" los secretos.
- Para la autenticación en Azure se utiliza un "DefaultAzureCredential" de ["Azure.Identity"](https://azuresdkdocs.blob.core.windows.net/$web/dotnet/Azure.Identity/1.9.0/api/index.html)
- Para la autorización la identidad requere el rol "Key Vault Secrets User" en el Key Vault.

---

## 2 .Net Framework

- Se utiliza https://github.com/aspnet/MicrosoftConfigurationBuilders.
- Las propiedades se combinan con la siguiente prioridad: 1. keyvault, 2. user secret, 3. app settings.

### 2.1 User Secrets

- Referenciar el paquete "Microsoft.Configuration.ConfigurationBuilders.UserSecrets".
- Verificar la inclusión en el archivo de configuración de la sección "configBuilders" y del builder "Secrets".
- Utilizar la propiedad userSecretsId en el archivo de configuración si se desea utilizar un ID personalizado o un GUID. En este caso los secretos se almacena en formato XML en la ruta "%APPDATA%\Microsoft\UserSecrets\<ID>\secrets.json".
- Utilizar la propiedad userSecretsFile en el archivo de configuración si se desea utilizar una ruta personalizada. En este caso los secretos se almacena en formato XML en la ruta personalizada.
- Ajustar el elemento appSettings con el atributo configBuilders="Secrets".
- Las propiedades que van a estar en los secretos deben ser establecidas en el appSettings vacias.

### 2.2 Key Vault Secrets

- Referenciar "Microsoft.Configuration.ConfigurationBuilders.Azure".
- Verificar la inclusión en el archivo de configuración de la sección "configBuilders" y del builder "AzureKeyVault".
- Ajustar el atributo vaultName con el nomnre del Key Vault.
- Ajustar el elemento appSettings con el atributo configBuilders="AzureKeyVault".
- Las propiedades que van a estar en los secretos deben ser establecidas en el appSettings vacias.
- Para la autenticación en Azure se utiliza un "DefaultAzureCredential" de ["Azure.Identity"](https://azuresdkdocs.blob.core.windows.net/$web/dotnet/Azure.Identity/1.9.0/api/index.html)
- Para la autorización la identidad requere el rol "Key Vault Secrets User" en el Key Vault.
