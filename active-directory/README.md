# example-azure / active-directory

- [Comandos](#comandos)
- [Asignación de Roles](#asignación-de-roles)

---

## Comandos

Comandos generales para la administración de un Active Directory.

```powershell
#crear grupo
az ad group create --display-name "luiscasalas16-group" --mail-nickname "luiscasalas16-group" --description "luiscasalas16-group"
```

---

## Asignación de Roles

Esta sección describe los comandos necesarios para asignar roles mediante Azure CLI. (link)[https://learn.microsoft.com/en-us/azure/role-based-access-control/role-assignments-cli]

```powershell
#

# 1 - determinar la identity
#  users
az ad user list --query '[].{Id:id, DisplayName:displayName}' --out table
#  groups
az ad group list --query '[].{Id:id, DisplayName:displayName}' --out table
#  service principal, <name> = nombre del service principal
az ad sp list --all --query "[?contains(displayName,'<name>')].{Id:id, DisplayName:displayName}" --out table

# 2 - determinar el rol, <name> = nombre del rol
az role definition list --query "[?contains(roleName,'<name>')].{Name:name, RoleName:roleName}" --out table

# 3 - determinar el scope
#  resource scope: need the ID of the resource, <name> = nombre del recurso
az resource list --name 'luiscasalas16-key-vault' --query "[].{Id:id}" --out tsv
#  resource group scope: need the name of the resource group.
az group list --query "[].{Name:name}" --out table
#  subscription scope: need the ID of the subscription.
az account list --query "[].{Id:id, Name:name}" --out table
#  management group scope: need the name of the management group.
az account management-group list --query "[].{Name:name}" --out table

# 4 - ejecutar
#  resource scope
az role assignment create --assignee "<identity ID>" --role "<role name>" --scope "<resource ID>"
#  resource group scope
az role assignment create --assignee "<identity ID>" --role "<role name>" --resource-group "<resource group name>"
#  subscription scope
az role assignment create --assignee "<identity ID>" --role "<role name>" --subscription "<subscription ID>"
#  management group scope
az role assignment create --assignee "<identity ID>" --role "<role name>" --scope "/providers/Microsoft.Management/managementGroups/<management group name>"
```
