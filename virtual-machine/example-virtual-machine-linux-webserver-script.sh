#!/bin/sh

# actualizar e instalar nginx, .net, zip
sudo apt-get update
sudo apt-get install -y nginx
sudo apt-get install -y aspnetcore-runtime-7.0
sudo apt-get install -y zip 

# carpeta aplicación
sudo mkdir /var/www/app
sudo chmod 777 /var/www/app

# instalar aplicación
cd /var/www/app
wget https://raw.githubusercontent.com/luiscasalas16/notes-azure/main/_dist/NetVirtualMachineWebMvc.zip -O NetVirtualMachineWebMvc.zip
unzip NetVirtualMachineWebMvc.zip
rm NetVirtualMachineWebMvc.zip

# configurar nginx
cd /etc/nginx/sites-available
sudo rm default
sudo touch default
sudo chmod 777 default
echo 'server {' >> default
echo '    listen        80;' >> default
echo '        location / {' >> default
echo '            proxy_pass         http://127.0.0.1:5000;' >> default
echo '            proxy_http_version 1.1;' >> default
echo '            proxy_set_header   Upgrade $http_upgrade;' >> default
echo '            proxy_set_header   Connection keep-alive;' >> default
echo '            proxy_set_header   Host $host;' >> default
echo '            proxy_cache_bypass $http_upgrade;' >> default
echo '            proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;' >> default
echo '            proxy_set_header   X-Forwarded-Proto $scheme;' >> default
echo '        }' >> default
echo '}' >> default

# reiniciar nginx
sudo nginx -s reload

# configurar servicio
cd /etc/systemd/system
sudo touch kestrel-app.service
sudo chmod 777 kestrel-app.service
echo '[Unit]' >> kestrel-app.service
echo 'Description=ASP.NET Core Web App running on Ubuntu' >> kestrel-app.service
echo '[Service]' >> kestrel-app.service
echo 'WorkingDirectory=/var/www/app' >> kestrel-app.service
echo 'ExecStart=/usr/bin/dotnet /var/www/app/NetVirtualMachineWebMvc.dll' >> kestrel-app.service
echo 'Restart=always' >> kestrel-app.service
echo 'RestartSec=10' >> kestrel-app.service
echo 'KillSignal=SIGINT' >> kestrel-app.service
echo 'SyslogIdentifier=dotnet-web-app' >> kestrel-app.service
echo 'User=www-data' >> kestrel-app.service
echo 'Environment=ASPNETCORE_ENVIRONMENT=Production' >> kestrel-app.service
echo '[Install]' >> kestrel-app.service
echo 'WantedBy=multi-user.target' >> kestrel-app.service

# iniciar servicio
sudo systemctl enable kestrel-app.service
sudo systemctl start kestrel-app.service
