#!/usr/bin/env pwsh

Set-StrictMode -Version latest
$ErrorActionPreference = "Stop"

$component = Get-Content -Path "component.json" | ConvertFrom-Json
$image="$($component.registry)/$($component.name):$($component.version)-$($component.build)-rc"

# Build docker image
docker build -f docker/Dockerfile -t $image .

# Set environment variables
$env:IMAGE = $image

try {
    # Workaround to remove dangling images
    docker-compose -f ./docker/docker-compose.yml down

    docker-compose -f ./docker/docker-compose.yml up -d

    # Test using curl
    # curl http://localhost:8080/v1/limits/get_limits_by_filter -X POST -v
} finally {
    docker-compose -f ./docker/docker-compose.yml down
}

