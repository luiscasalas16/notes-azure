#!/bin/bash

# detener servicio
sudo systemctl stop kestrel-app.service

# carpeta aplicación
sudo rm -r /var/www/app
sudo mkdir /var/www/app
sudo chmod 777 /var/www/app

# instalar aplicación
cd /var/www/app
wget https://raw.githubusercontent.com/luiscasalas16/notes-azure/main/_dist/$application.zip -O $application.zip
unzip $application.zip
rm $application.zip

# configurar servicio
cd /etc/systemd/system
sudo rm kestrel-app.service
sudo touch kestrel-app.service
sudo chmod 777 kestrel-app.service
echo "[Unit]" >> kestrel-app.service
echo "Description=ASP.NET Core Web App running on Ubuntu" >> kestrel-app.service
echo "[Service]" >> kestrel-app.service
echo "WorkingDirectory=/var/www/app" >> kestrel-app.service
echo "ExecStart=/usr/bin/dotnet /var/www/app/$application.dll" >> kestrel-app.service
echo "Restart=always" >> kestrel-app.service
echo "RestartSec=10" >> kestrel-app.service
echo "KillSignal=SIGINT" >> kestrel-app.service
echo "SyslogIdentifier=dotnet-web-app" >> kestrel-app.service
echo "User=www-data" >> kestrel-app.service
echo "Environment=ASPNETCORE_ENVIRONMENT=Production" >> kestrel-app.service
echo "[Install]" >> kestrel-app.service
echo "WantedBy=multi-user.target" >> kestrel-app.service

# iniciar servicio
sudo systemctl daemon-reload
sudo systemctl start kestrel-app.service
