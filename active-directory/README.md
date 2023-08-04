# example-azure / active-directory

- [Comandos para Grupos](#comandos-para-grupos)
- [Comandos para Identidades](#comandos-para-identidades)
- [Comandos para Roles](#comandos-para-roles)

---

## Comandos para Grupos

```powershell
# crear grupo
az ad group create --display-name "luiscasalas16-group" --mail-nickname "luiscasalas16-group" --description "luiscasalas16-group"
```

---

## Comandos para Identidades

Referencias:

- [Application service principal](https://learn.microsoft.com/en-us/azure/active-directory/develop/app-objects-and-service-principals)
- [Managed identity service principal](https://learn.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview)
- [Best Practices](https://learn.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/managed-identity-best-practice-recommendations)

---

```powershell
# crear application identity
az ad sp create-for-rbac --name "luiscasalas16-application"
```

```powershell
# crear managed identity
az identity create --name "luiscasalas16-managed-identity" --resource-group "luiscasalas16-resource-group"
```

## Comandos para Roles

Referencias:

- [Role based Access Control](https://learn.microsoft.com/en-us/azure/role-based-access-control/)
- [Best Practices](https://learn.microsoft.com/en-us/azure/role-based-access-control/best-practices)

Comandos para obtener identificadores de objetos.

```powershell
# user ID
az ad user list --query '[].{ Id:id, DisplayName:displayName }' --out table
# group ID
az ad group list --query '[].{ Id:id, DisplayName:displayName }' --out table
# service principal ID, <name> = nombre del service principal
az ad sp list --all --query "[?contains(displayName,'luiscasalas16-managed-identity')].{ Id:id, DisplayName:displayName }" --out table

# managed identity ID, <name> = nombre del managed identity
az identity list --query "[?contains(name,'luiscasalas16-managed-identity')].{ Id:id }" --out tsv

# rol Name, <name> = nombre del rol
az role definition list --query "[?contains(roleName,'<name>')].{ Name:name, RoleName:roleName }" --out table

# resource ID, <name> = nombre del resource
az resource list --name '<name>' --query "[].{ Id:id }" --out tsv
# resource group Name
az group list --query "[].{ Name:name }" --out table
# subscription ID
az account list --query "[].{ Id:id, Name:name }" --out table
# management groups Name
az account management-group list --query "[].{ Name:name }" --out table
```

Comandos para revisar roles.

```powershell
# users or groups or service principals
az role assignment list --all --assignee "(<user | group | service principal) ID>"
# resource group
az role assignment list --all --resource-group "<resource group Name>"
# subscription
az role assignment list --all --subscription "<subscription ID>"
# management group scope
az role assignment list --all --scope "/providers/Microsoft.Management/managementGroups/<management group name>"
```

Comandos para otorgar roles.

```powershell
# resource scope
az role assignment create --assignee "(<user | group | service principal) ID>" --role "<role Name>" --scope "<resource ID>"
# resource group scope
az role assignment create --assignee "(<user | group | service principal) ID>" --role "<role Name>" --resource-group "<resource group Name>"
# subscription scope
az role assignment create --assignee "(<user | group | service principal) ID>" --role "<role Name>" --subscription "<subscription ID>"
# management group scope
az role assignment create --assignee "(<user | group | service principal) ID>" --role "<role Name>" --scope "/providers/Microsoft.Management/managementGroups/<management group Name>"
```

Comandos para revocar roles.

```powershell
# resource scope
az role assignment delete --assignee "(<user | group | service principal) ID>" --role "<role Name>" --scope "<resource ID>"
# resource group scope
az role assignment delete --assignee "(<user | group | service principal) ID>" --role "<role Name>" --resource-group "<resource group Name>"
# subscription scope
az role assignment delete --assignee "(<user | group | service principal) ID>" --role "<role Name>" --subscription "<subscription ID>"
# management group scope
az role assignment delete --assignee "(<user | group | service principal) ID>" --role "<role Name>" --scope "/providers/Microsoft.Management/managementGroups/<management group Name>"
```
