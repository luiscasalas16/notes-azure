# example-azure / active-directory

- [Comandos para Grupos](#comandos-para-grupos)
- [Comandos para Roles](#comandos-para-roles)

---

## Comandos para Grupos

Comandos generales para la administración de un Active Directory.

```powershell
#crear grupo
az ad group create --display-name "luiscasalas16-group" --mail-nickname "luiscasalas16-group" --description "luiscasalas16-group"
```

---

## Comandos para Roles

(link)[https://learn.microsoft.com/en-us/azure/role-based-access-control/]

Comandos para obtener identificadores de objetos.

```powershell
# users
az ad user list --query '[].{Id:id, DisplayName:displayName}' --out table
# groups
az ad group list --query '[].{Id:id, DisplayName:displayName}' --out table
# service principals, <name> = nombre del service principal
az ad sp list --all --query "[?contains(displayName,'<name>')].{Id:id, DisplayName:displayName}" --out table

# rol, <name> = nombre del rol
az role definition list --query "[?contains(roleName,'<name>')].{Name:name, RoleName:roleName}" --out table

#  resources
az resource list --name 'luiscasalas16-key-vault' --query "[].{Id:id}" --out tsv
#  resource groups
az group list --query "[].{Name:name}" --out table
#  subscriptions
az account list --query "[].{Id:id, Name:name}" --out table
#  management groups
az account management-group list --query "[].{Name:name}" --out table
```

Comandos para revisar roles.

```powershell
#  users or groups or service principals
az role assignment list --assignee "<user ID | group ID | service principal ID>"
#  resource group
az role assignment list --resource-group "<resource group name>"
#  subscription
az role assignment list --subscription "<subscription ID>"
#  management group scope
az role assignment list --scope "/providers/Microsoft.Management/managementGroups/<management group name>"
```

Comandos para otorgar roles.

```powershell
#  resource scope
az role assignment create --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --scope "<resource ID>"
#  resource group scope
az role assignment create --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --resource-group "<resource group name>"
#  subscription scope
az role assignment create --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --subscription "<subscription ID>"
#  management group scope
az role assignment create --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --scope "/providers/Microsoft.Management/managementGroups/<management group name>"
```

Comandos para revocar roles.

```powershell
#  resource scope
az role assignment delete --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --scope "<resource ID>"
#  resource group scope
az role assignment delete --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --resource-group "<resource group name>"
#  subscription scope
az role assignment delete --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --subscription "<subscription ID>"
#  management group scope
az role assignment delete --assignee "<user ID | group ID | service principal ID>" --role "<role name>" --scope "/providers/Microsoft.Management/managementGroups/<management group name>"
```
