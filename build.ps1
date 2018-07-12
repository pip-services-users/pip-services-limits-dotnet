#!/usr/bin/env pwsh

Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

$component = Get-Content -Path "component.json" | ConvertFrom-Json
$image="$($component.registry)/$($component.name):$($component.version)-$($component.build)"
$container="$($component.name)"

# Set environment variables
$env:IMAGE = $image
$env:CONTAINER = $container

# Remove build files
rm -rf ./obj

# Build docker image
docker build -f docker/Dockerfile.build -t ${IMAGE} .

# Create and copy compiled files, then destroy
docker create --name ${CONTAINER} ${IMAGE}
docker cp ${CONTAINER}:/obj ./obj
docker rm ${CONTAINER}