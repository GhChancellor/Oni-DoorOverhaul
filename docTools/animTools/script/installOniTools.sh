#!/bin/bash
# https://github.com/GhChancellor/Oni-DoorOverhaul
# Install Docker, Spriter r10 on Ubuntu 20.04 LTS, kanimalse, VSC, Dotnet and sdk 8
# SPDX-License-Identifier: MIT

set -eu

WORK_DIR=$(pwd)

# install VSC & DOTNET 8
sudo chmod +x "${WORK_DIR}/./VSC_Net8.sh"
# install Docker
sudo chmod +x "${WORK_DIR}/./installDocker.sh"
# install Kanimalse
sudo chmod +x "${WORK_DIR}/./installKanimalse.sh"
# install SpriterR10
sudo chmod +x "${WORK_DIR}/./installSpriterR10.sh"

# # echo "install VSC & DOTNET 8"
./VSC_Net8.sh

# # echo "Install Docker"
./installDocker.sh

# echo "install Kanimalse"
./installKanimalse.sh

# echo "Install Spriter R10 (64)"
./installSpriterR10.sh
