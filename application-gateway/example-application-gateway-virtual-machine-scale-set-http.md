# example-application-gateway-virtual-machine-scale-set-http

```powershell

##### virtual network #####

# crear virtual network
az network vnet create --name "lcs16-vn" --resource-group "lcs16-rg" `
    --address-prefix 10.0.0.0/16
# crear virtual network subnet
az network vnet subnet create --vnet-name "lcs16-vn" --resource-group "lcs16-rg" `
    --name "agSubnet" --address-prefixes 10.0.1.0/24
az network vnet subnet create --vnet-name "lcs16-vn" --resource-group "lcs16-rg" `
    --name "backendSubnet" --address-prefixes 10.0.2.0/24

##### application-gateway #####

# crear ip para application-gateway
az network public-ip create --name "lcs16-ip-ag" --resource-group "lcs16-rg" --location "eastus" `
    --version "IPv4" --sku "Standard" --allocation-method "Static" --zone 1 --tier "Regional" --dns-name "lcs16-ip-ag"

#--sku "WAF_v2" --waf-policy "lcs16-wp"

# crear application gateway
az network application-gateway create --name "lcs16-ag" --resource-group "lcs16-rg" --location "eastus" `
    --sku "Standard_v2" --capacity 1 `
    --vnet-name "lcs16-vn" --subnet "agSubnet" --public-ip-address "lcs16-ip-ag" `
    --priority 1000 --http-settings-port 80 --http-settings-protocol "Http" `
    --frontend-port 80

##### virtual machine scale set #####

# crear virtual machine scale set
"n" | ssh-keygen -t rsa -b 4096 -C "azureadministrator" -f "$ENV:UserProfile/.ssh/lcs16-vmss" -P "azureprueba123*"
az vmss create --name "lcs16-vmss" --resource-group "lcs16-rg" --orchestration-mode "flexible" --image "Canonical:0001-com-ubuntu-server-jammy:22_04-lts-gen2:latest" --vm-sku "Standard_B2ms" --admin-username "azureadministrator" --ssh-key-values "~/.ssh/lcs16-vmss.pub" --os-disk-size-gb 32 --instance-count 2 --vnet-name "lcs16-vn" --subnet "backendSubnet" --backend-pool-name "appGatewayBackendPool"

# publicar aplicación en cada instancia del virtual machine scale set
$vmssInstances = az vmss list-instances --name "lcs16-vmss" --resource-group "lcs16-rg" --query "[].name"
$vmssInstances | ConvertFrom-Json | ForEach-Object -Parallel {
  Write-Host  "exec $_"
  az vm run-command create --name "publish" --vm-name $_ --resource-group "lcs16-rg" --script-uri "https://raw.githubusercontent.com/luiscasalas16/notes-azure/main/virtual-machine/example-virtual-machine-linux-webserver-script.sh"
}

```
