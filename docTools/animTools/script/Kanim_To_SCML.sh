#!/bin/bash
# Kanim To SCML
# https://github.com/GhChancellor/Oni-DoorOverhaul
# SPDX-License-Identifier: MIT

# Defination of variables
NAMEFILE="trap_door_manual"

# Find Documents directory
ONI_TOOLS="$(xdg-user-dir DOCUMENTS)/OniGraphicTool"
WORK_DIR="${ONI_TOOLS}/docker-kanimal-cli"
SOURCE_DIR="/data/Source"
DESTINATION_DIR="/data/Destination"

# Name file
PNG_FILE="${NAMEFILE}_0.png"
BUILD_FILE="${NAMEFILE}_build.bytes"
ANIM_FILE="${NAMEFILE}_anim.bytes"

# Run Docker
docker run -it --rm \
  -v "${WORK_DIR}:/data" \
  kanimal-cli scml \
  "${SOURCE_DIR}/${PNG_FILE}" \
  "${SOURCE_DIR}/${BUILD_FILE}" \
  "${SOURCE_DIR}/${ANIM_FILE}" \
  --output "${DESTINATION_DIR}"

echo ">>>>>>>>>>>>>>>>>>>>>>> ${WORK_DIR}/Destination"
