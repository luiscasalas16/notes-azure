# curso-redes

```powershell
$whatismyipaddress = (Invoke-WebRequest -uri "http://ifconfig.me/ip").Content

# crear grupo de recursos networks
az group create --name "lcs16-rg-networks-eastus2" --location "eastus2"

# crear virtual network vn1
az network vnet create --name "lcs16-vn1-eastus2" --resource-group "lcs16-rg-networks-eastus2" --address-prefix 172.16.0.0/16 --subnet-name "lcs16-vn1-sub-internal-services" --subnet-prefixes 172.16.0.0/24 --ddos-protection "false"

$subnet_internal_services = $(az network vnet subnet show --name "lcs16-vn1-sub-internal-services" --vnet-name "lcs16-vn1-eastus2" --resource-group "lcs16-rg-networks-eastus2" --query id -o tsv)

# crear grupo de recursos servers
az group create --name "lcs16-rg-servers-eastus2" --location "eastus2"

# crear virtual machine web-01
az vm create --name "lcs16-vm-web-01" --resource-group "lcs16-rg-servers-eastus2" --location "eastus2" --image "MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition-smalldisk:latest" --size "Standard_B2s" --admin-username "azureadministrator" --admin-password "azureprueba123*" --os-disk-name "lcs16-vm-web-01OsDisk" --os-disk-size-gb 32 --public-ip-address-dns-name "lcs16-vm-web-01" --subnet $subnet_internal_services --private-ip-address 172.16.0.10 --public-ip-sku "Standard"

# habilitar auto-shutdown virtual machine web-01
az vm auto-shutdown --name "lcs16-vm-web-01" --resource-group "lcs16-rg-servers-eastus2" --time 0000

# permitir acceso por rdp a virtual machine web-01 de ip específica
az network nsg rule update --name "rdp" --nsg-name "lcs16-vm-web-01NSG" --resource-group "lcs16-rg-servers-eastus2" --source-address-prefixes $whatismyipaddress --destination-address-prefixes 172.16.0.10

# permitir acceso por http a virtual machine web-01 de *
az network nsg rule create --name "http" --nsg-name "lcs16-vm-web-01NSG" --resource-group "lcs16-rg-servers-eastus2"--priority 100 --source-address-prefixes "*" --source-port-ranges "*" --destination-address-prefixes 172.16.0.10 --destination-port-ranges 80 --access Allow --protocol Tcp

# permitir Echo en firewall de virtual machine web-01
Enable-NetFirewallRule -displayName "File and Printer Sharing (Echo Request - ICMPv4-In)"
Enable-NetFirewallRule -displayName "File and Printer Sharing (Echo Request - ICMPv6-In)"
Enable-NetFirewallRule -displayName "File and Printer Sharing (Echo Request - ICMPv4-Out)"
Enable-NetFirewallRule -displayName "File and Printer Sharing (Echo Request - ICMPv6-Out)"

# permitir 80 en firewall de virtual machine web-01
Enable-NetFirewallRule -displayName "World Wide Web Services (HTTP Traffic-In)"

# crear ip
az network public-ip create --name "lcs16-ip-nat-vn-eastus2" --resource-group "lcs16-rg-networks-eastus2" --sku "Standard"
# crear nat gateway
az network nat gateway create --name "lcs16-nat-vn-eastus2" --resource-group "lcs16-rg-networks-eastus2" --public-ip-addresses "lcs16-ip-nat-vn-eastus2" --idle-timeout 5
# establecer nat gateway
az network vnet subnet update --name "lcs16-vn1-sub-internal-services" --vnet-name "lcs16-vn1-eastus2" --resource-group "lcs16-rg-networks-eastus2" --nat-gateway "lcs16-nat-vn-eastus2"

# crear virtual network vn2
az network vnet create --name "lcs16-vn2-eastus2" --resource-group "lcs16-rg-networks-eastus2" --address-prefix 172.17.0.0/16 172.18.0.0/16 --subnet-name "lcs16-vn2-sub-internal-services" --subnet-prefixes 172.17.0.0/24 --ddos-protection "false"
```
