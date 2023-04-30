# webjobs

Proyecto de ejemplo de Key Vault.

en CMD
	az login
	az group create --name "luiscasalas16-resource-group" --location "eastus2"
	az keyvault create --name "luiscasalas16-key-vault" --resource-group "luiscasalas16-resource-group" --location "eastus2"
	az keyvault secret set --vault-name "luiscasalas16-key-vault" --name "MySecretName" --value "MySecretValue"

en VS
	Connected Services -> Add -> Azure Key Vault

	
    <UserSecretsId>test-user-secrets</UserSecretsId>
    <!--<UserSecretsId>6b3f8603-e958-4a71-a99f-ceca7301871b</UserSecretsId>-->


Los pasos para habilitar son:
-   Configurar application service.
-   Configurar storage account.
-   Obtener el access key del storage account.
-   Registrar en la configuración del application service un connection string con la llave AzureWebJobsDashboard y el valor del access key del storage account.
-   Publicar y registrar los web jobs.
