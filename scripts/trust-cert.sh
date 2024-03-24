#!/usr/bin/env bash

###################################################################################
#                                                                                 #
# From https://forum.manjaro.org/t/ssl-cerificates-for-local-development/139354/5 #
#                                                                                 #
###################################################################################

# This is a convenience script to trust the necessary ports on localhost to run the
# Quizim web app locally on your machine, for development purposes. Review this
# script to make sure the paths line up with your setup, and run at your own risk!

set -e

cd $HOME || exit

# e.g. aspnet dev cert is located at /usr/local/share/ca-certificates/aspnet/
CERT_HOME="/usr/local/share/ca-certificates"

dotnet dev-certs https
sudo -E dotnet dev-certs https -ep localhost.crt --format Pem

# Setup Firefox
echo "{
    \"policies\": {
        \"Certificates\": {
            \"Install\": [
            	\"aspnetcore-localhost-https.crt\"
            ]
        }
    }
}" > policies.json

# Trust Firefox
sudo mv policies.json /usr/lib/firefox/distribution/
mkdir -p ~/.mozilla/certificates
sudo cp localhost.crt ~/.mozilla/certificates/aspnetcore-localhost-https.crt

# Trust Chromium based browsers
sudo certutil -d sql:$HOME/.pki/nssdb -A -t "P,," -n localhost -i ./localhost.crt
sudo certutil -d sql:$HOME/.pki/nssdb -A -t "C,," -n localhost -i ./localhost.crt

sudo trust anchor --store localhost.crt

# # Trust wget
# sudo cp localhost.crt $CERT_HOME/aspnet/https.crt
# sudo cp localhost.crt $CERT_HOME/trust-source/anchors/aspnetcore-https-localhost.pem
# sudo cp localhost.crt $CERT_HOME/aspnet/aspnetcore-https-localhost.pem
# sudo chmod 666 $CERT_HOME/trust-source/anchors/aspnetcore-https-localhost.pem
# sudo chmod 666 $CERT_HOME/aspnet/aspnetcore-https-localhost.pem

# Trust dotnet-to-dotnet
sudo cp localhost.crt /etc/ssl/certs/aspnetcore-https-localhost.pem

# Clean up
sudo update-ca-trust
sudo update-ca-trust extract
rm -f localhost.crt
