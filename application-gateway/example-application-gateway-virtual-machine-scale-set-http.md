# example-application-gateway-virtual-machine-scale-set-http

Ejemplo de configuración de un application gateway con waf cómo puerta de entrada para un virtual machine scale set con 2 instancias.

- Se publica el application gateway por HTTP.
- Se distribuye el tráfico entre las instancias del virtual machine scale set.
- Se permite sólo el tráfico desde el application gateway en el virtual machine scale set.

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

# crear waf policy
az network application-gateway waf-policy create --name "lcs16-wp" --resource-group "lcs16-rg" `
    --location "eastus" --type "OWASP" --version "3.2"
# configurar waf policy
az network application-gateway waf-policy policy-setting update --policy-name "lcs16-wp" --resource-group "lcs16-rg" `
    --state "Enabled" --mode "Prevention" --request-body-check true

# crear ip para application-gateway
az network public-ip create --name "lcs16-ip-ag" --resource-group "lcs16-rg" --location "eastus" `
    --version "IPv4" --sku "Standard" --allocation-method "Static" --tier "Regional" --dns-name "lcs16-application-gateway"

# crear application gateway
az network application-gateway create --name "lcs16-ag" --resource-group "lcs16-rg" --location "eastus" `
    --sku "WAF_v2" --waf-policy "lcs16-wp" --capacity 1 `
    --vnet-name "lcs16-vn" --subnet "agSubnet" --public-ip-address "lcs16-ip-ag" `
    --priority 1000 --http-settings-port 80 --http-settings-protocol "Http" `
    --frontend-port 80

##### virtual machine scale set #####

# crear virtual machine scale set
"n" | ssh-keygen -t rsa -b 4096 -C "azureadministrator" -f "$ENV:UserProfile/.ssh/lcs16-vmss" -P "azureprueba123*"
az vmss create --name "lcs16-vmss" --resource-group "lcs16-rg" --orchestration-mode "Uniform" --image "Canonical:0001-com-ubuntu-server-jammy:22_04-lts-gen2:latest" --vm-sku "Standard_B2ms" --admin-username "azureadministrator" --ssh-key-values "~/.ssh/lcs16-vmss.pub" --os-disk-size-gb 32 --instance-count 2 --vnet-name "lcs16-vn" --subnet "backendSubnet" --app-gateway "lcs16-ag" --backend-pool-name "appGatewayBackendPool"

# publicar aplicación en cada instancia del virtual machine scale set
$vmssInstances = az vmss list-instances --name "lcs16-vmss" --resource-group "lcs16-rg" --query "[].instanceId"
$vmssInstances | ConvertFrom-Json | ForEach-Object -Parallel {
    Write-Host  "exec $_"
    $result = Invoke-AzVmssVMRunCommand -ResourceGroupName 'lcs16-rg' -VMScaleSetName "lcs16-vmss" -InstanceId $_ -CommandId 'RunShellScript' -ScriptPath '.\virtual-machine\example-virtual-machine-linux-webserver-script.sh'
    Write-Output $result.Value
}

```
