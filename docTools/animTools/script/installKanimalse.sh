#!/bin/bash
# Install kanimalse
# https://github.com/GhChancellor/Oni-DoorOverhaul
# SPDX-License-Identifier: MIT

set -eu

sudo systemctl restart docker
echo "Wait 3 seconds"
sleep 3

# **********************************************************************************

# Find Documents directory
ONI_TOOLS="$(xdg-user-dir DOCUMENTS)/OniGraphicTool"
KANIMAL_SE_INSTALL="$ONI_TOOLS/docker-kanimal-cli"
BIN="bin"
TMP="$(mktemp -d)"
QUALCOSAINIT=$(pwd)

# Download kanimalse
URL_KANIMAL_SE="https://github.com/skairunner/kanimal-SE/releases/download/1.3.26/Linux.NET.dependent.zip"

# Make work dir of the kanimalse
echo "Make work dir $KANIMAL_SE_INSTALL"
mkdir -p "${ONI_TOOLS}"
mkdir -p "${KANIMAL_SE_INSTALL}"
mkdir -p "${KANIMAL_SE_INSTALL}/Destination"
mkdir -p "${KANIMAL_SE_INSTALL}/Source"
mkdir -p "${KANIMAL_SE_INSTALL}/${BIN}"

# Download kanimalse
echo "Download"
cd $TMP
curl -L -O "${URL_KANIMAL_SE}"

# Extract kanimalse
echo "Install kanimalse in ${KANIMAL_SE_INSTALL}/${BIN}"
unzip -q "Linux.NET.dependent.zip" -d "linux.net.dependent"
cd $TMP/"linux.net.dependent"
mv * "${KANIMAL_SE_INSTALL}/${BIN}"
rm -rf $TMP




# **********************************************************************************

# Create Dockerfile
echo "Creating Dockerfile"
cat << EOF > "${KANIMAL_SE_INSTALL}/${BIN}/Dockerfile"
# Use the official .NET Core 3.1 runtime as a base image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1

# Update package lists, install libgdiplus (a library for System.Drawing),
# then clean up to reduce image size
RUN apt update && apt install -y libgdiplus && rm -rf /var/lib/apt/lists/*

# Set the working directory in the container
WORKDIR /app

# Copy the kanimal-cli executable to the container
COPY ./* /app/

# Make the kanimal-cli executable
RUN chmod +x /app/kanimal-cli

# Set the entry point for the container
ENTRYPOINT ["/app/kanimal-cli"]
EOF

echo "Dockerfile created at ${KANIMAL_SE_INSTALL}/${BIN}/Dockerfile"

# Build DockerFile
cd ${KANIMAL_SE_INSTALL}/${BIN}

sudo docker build -t kanimal-cli .
sudo docker run -it --rm kanimal-cli




# **********************************************************************************

echo "Copy SCML_To_Kanim.sh & Kanim_To_SCML.sh to ${KANIMAL_SE_INSTALL}"
cd ${QUALCOSAINIT}

cp SCML_To_Kanim.sh ${KANIMAL_SE_INSTALL}
cp Kanim_To_SCML.sh ${KANIMAL_SE_INSTALL}

sudo chmod +x "${KANIMAL_SE_INSTALL}/SCML_To_Kanim.sh"
sudo chmod +x "${KANIMAL_SE_INSTALL}/Kanim_To_SCML.sh"

cat << EOF > "${KANIMAL_SE_INSTALL}/ReadMe.md"

Change NAMEFILE=""

SCML_To_Kanim.sh: Convert SCML file to KAnim
Example:

NAMEFILE="trap_door_manual.scml"


Kanim_To_SCML.sh: Convert KAnim file to SCML
Example:

NAMEFILE="trap_door_manual"

EOF

echo "Kanimalse has been installed"
