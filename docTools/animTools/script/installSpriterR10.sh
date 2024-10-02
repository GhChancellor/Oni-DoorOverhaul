#!/bin/bash
# Install Spriter r10 on Ubuntu 20.04 LTS
# https://github.com/GhChancellor/Oni-DoorOverhaul
#
# Thank you oxr463
# https://gist.github.com/oxr463/dce55b2a7e364f42ebb92891dc31ef7e
# SPDX-License-Identifier: MIT

set -eu

# Find Documents directory
SPRITER_DIR="SpriterR10(64)"
ONI_TOOLS="$(xdg-user-dir DOCUMENTS)/OniGraphicTool"
SPRITER_INSTALL="${ONI_TOOLS}/${SPRITER_DIR}"
TMP="$(mktemp -d)"

# Download Spriter archive and missing dependencies
URL_SPRITER10_64=("
  https://brashmonkey.com/brashmonkey/spriter/linux/Spriter_free_R10.tar.gz
  http://archive.ubuntu.com/ubuntu/pool/main/libp/libpng/libpng12-0_1.2.54-1ubuntu1.1_amd64.deb
  http://archive.ubuntu.com/ubuntu/pool/universe/g/gstreamer0.10/libgstreamer0.10-0_0.10.36-1.5ubuntu1_amd64.deb
  http://archive.ubuntu.com/ubuntu/pool/universe/g/gst-plugins-base0.10/libgstreamer-plugins-base0.10-0_0.10.36-2ubuntu0.2_amd64.deb"
)

# Make Spriter work dir
echo "Make work dir $SPRITER_INSTALL"
mkdir -p $ONI_TOOLS

# Download Spriter
echo "Download and install Spriter"
cd $TMP

for url in ${URL_SPRITER10_64};
do
  curl -L -O "${url}"
done

# Extract libraries from dependency packages
DEB="$(find . -name '*.deb')"

for deb in ${DEB};
do
  DEB_DIR="$(echo ${deb} | sed 's/_amd64\.deb//g')"
  mkdir "${DEB_DIR}"
  cd "${DEB_DIR}"
  cp "../${deb}" .
  ar x *.deb
  tar xf data.tar.xz
  cd ..
done

# Remove dangling symlinks
find . -xtype l -exec rm {} \;

# Extract Spriter
tar xf "Spriter_free_R10.tar.gz"

# Copy dependency libraries to Spriter directory
find "$TMP" -name "*.so*" -exec cp --update=none  "{}" "$TMP/${SPRITER_DIR}" \;

# Copy Spriter directory to working directory
cp -R "$TMP/${SPRITER_DIR}" "$SPRITER_INSTALL"

# Clean up temporary files
rm -rf "${TMP}"

echo "Spriter has been installed"
