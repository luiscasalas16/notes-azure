#!/bin/sh

# actualizar e instalar nginx y .net
sudo apt-get update
sudo apt-get install -y nginx
sudo apt-get install -y aspnetcore-runtime-7.0

# carpeta aplicación
sudo mkdir /var/www/app
sudo chmod 777 /var/www/app
