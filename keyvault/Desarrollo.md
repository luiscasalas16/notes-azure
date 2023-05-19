# Secretos

## 1. Net
- Secretos
	- Habilitar por Visual Studio en Manage User Secrets.
	- Ajustar el UserSecretsId en el proyecto. Se almacena en la ruta de datos del usuario.
	- Se referencia automáticamente Microsoft.Extensions.Configuration.UserSecrets 7.0.0.
	- Las propiedades se combinan con la siguiente prioridad: keyvault - user secret - app settings.
- Key Vault
	- Referenciar Azure.Extensions.AspNetCore.Configuration.Secrets 1.2.2.
	- Referenciar Azure.Identity 1.8.2.
	- Incluir en configuración con método AddAzureKeyVault.
	- Las propiedades se combinan con la siguiente prioridad: keyvault - user secret - app settings.

## 2. Net Fw
- Requiere .Net Framework 4.7.1.
- Se utiliza https://github.com/aspnet/MicrosoftConfigurationBuilders.
- Secretos
	- Referenciar Microsoft.Configuration.ConfigurationBuilders.UserSecrets 3.0.0.
	- Incluir configSections y configBuilders (se puede hacer por userSecretsId en la ruta de datos del usuario o por userSecretsFile en una ruta específica).
	- Ajustar el appSettings con el configBuilders="Secrets"
	- Las propiedades deben estar establecidas en el appSettings para que sean reemplazadas.
- Key Vault
	- Referenciar Microsoft.Configuration.ConfigurationBuilders.Azure 3.0.0.
	- Incluir configSections y configBuilders.
	- Ajustar el appSettings con el configBuilders="AzureKeyVault"
	- Las propiedades deben estar establecidas en el appSettings para que sean reemplazadas.