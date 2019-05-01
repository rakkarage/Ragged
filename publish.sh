sudo dotnet publish -c Release -o /var/www/html/Ragged
sudo systemctl restart nginx
sudo cp Ragged.service /etc/systemd/system
