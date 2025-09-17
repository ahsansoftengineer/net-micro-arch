#!/bin/bash

services=(
    "../SBA.APIGateway/SBA.APIGateway.csproj"
    "../SBA.Auth/SBA.Auth.csproj"
    "../SBA.Hierarchy/SBA.Hierarchy.csproj"
    "../SBA.Userz/SBA.Userz.csproj"
    "../SBA.Orderz/SBA.Orderz.csproj"
)

for service in "${services[@]}"; do
    echo "Starting $service..."
    dotnet watch --project $service
    read -p "Press enter to continue to the next service..."
done
