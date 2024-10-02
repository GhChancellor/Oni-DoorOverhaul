#! /bin/bash
# VSC and .Net8
# https://github.com/GhChancellor/Oni-DoorOverhaul
# SPDX-License-Identifier: MIT

echo "Download and Install Visual Studio Code, dotnet sdk 8"

sudo apt install -y wget gpg apt-transport-https

wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > packages.microsoft.gpg
sudo install -D -o root -g root -m 644 packages.microsoft.gpg /etc/apt/keyrings/packages.microsoft.gpg
echo "deb [arch=amd64,arm64,armhf signed-by=/etc/apt/keyrings/packages.microsoft.gpg] https://packages.microsoft.com/repos/code stable main" |sudo tee /etc/apt/sources.list.d/vscode.list > /dev/null
rm -f packages.microsoft.gpg

sudo apt update
sudo apt install -y code dotnet-sdk-8.0

echo "VSC has been installed"
echo "Dotnet has been installed"
