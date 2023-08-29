#!/bin/sh

# instalar aplicación
cd /var/www/app
wget https://raw.githubusercontent.com/luiscasalas16/notes-azure/main/_dist/$1.zip -O $1.zip
unzip $1.zip
rm $1.zip

sudo nginx -s reload

# configurar servicio
cd /etc/systemd/system
sudo rm kestrel-app.service
sudo touch kestrel-app.service
sudo chmod 777 kestrel-app.service
echo '[Unit]' >> kestrel-app.service
echo 'Description=ASP.NET Core Web App running on Ubuntu' >> kestrel-app.service
echo '[Service]' >> kestrel-app.service
echo 'WorkingDirectory=/var/www/app' >> kestrel-app.service
echo 'ExecStart=/usr/bin/dotnet /var/www/app/$1.dl' >> kestrel-app.service
echo 'Restart=always' >> kestrel-app.service
echo 'RestartSec=10' >> kestrel-app.service
echo 'KillSignal=SIGINT' >> kestrel-app.service
echo 'SyslogIdentifier=dotnet-web-app' >> kestrel-app.service
echo 'User=www-data' >> kestrel-app.service
echo 'Environment=ASPNETCORE_ENVIRONMENT=Production' >> kestrel-app.service
echo '[Install]' >> kestrel-app.service
echo 'WantedBy=multi-user.target' >> kestrel-app.service

# reiniciar servicios
sudo nginx -s reload
sudo systemctl restart kestrel-app.service
