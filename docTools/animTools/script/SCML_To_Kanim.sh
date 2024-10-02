#!/bin/bash
# SCML To Kanim
# https://github.com/GhChancellor/Oni-DoorOverhaul
# SPDX-License-Identifier: MIT

# Defination of variables
NAMEFILE="trap_door_manual.scml"

# Find Documents directory
ONI_TOOLS="$(xdg-user-dir DOCUMENTS)/OniGraphicTool"
WORK_DIR="${ONI_TOOLS}/docker-kanimal-cli"
SOURCE_DIR="/data/Source"
DESTINATION_DIR="/data/Destination"

# Run Docker
docker run -it --rm \
  -v ${WORK_DIR}:/data \
  kanimal-cli kanim \
  ${SOURCE_DIR}/${NAMEFILE} \
  --output ${DESTINATION_DIR} \
  --interpolate

echo ">>>>>>>>>>>>>>>>>>>>>>> ${WORK_DIR}/Destination"
