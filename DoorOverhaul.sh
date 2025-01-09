#!/bin/bash

echo

NAME="DoorOverhaul.csproj"

echo -e "\e[32m ----------------------------- Removing bin and obj folders... ----------------------------- \e[0m"
rm -rf bin obj
echo -e "\e[32m Bin and obj folders removed. \e[0m"

echo -e "\e[32m ----------------------------- clean project... ----------------------------- \e[0m"
echo
dotnet clean "${NAME}"

echo -e "\e[32m Building project... \e[0m"

dotnet build "${NAME}" && (
    echo -e "\e[32m ----------------------------- Build succeeded! ----------------------------- \e[0m"
    echo -e "\e[32m ----------------------------- Init steam ----------------------------- \e[0m"
    steam steam://rungameid/457140
) || (
    echo -e "\e[31m ----------------------------- Build failed. Exiting script.----------------------------- \e[0m"
)
