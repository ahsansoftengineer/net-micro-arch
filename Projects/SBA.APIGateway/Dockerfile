# Build:
# docker build -t mansanhhub/sba-srvc-gateway -f Projects/SBA.APIGateway/Dockerfile .
# docker push mansanhhub/sba-srvc-gateway
# docker run -d --name sba-srvc-gateway -p 1100:1100 -e DOTNET_ENVIRONMENT=Docker mansanhhub/sba-srvc-gateway
# docker rm -f sba-srvc-gateway
# docker exec -it <container_id> /bin/bash

# http://localhost:1100/api/gateway/v1/swagger/index.html
# http://localhost:1100/api/gateway/v1/_projectz-lookup/gets-lookup

# ===========================================================

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy only .csproj files first (to enable Docker caching of restore)
# COPY GLOB/GLOB.API.Config/*.csproj ./GLOB/GLOB.API.Config/
COPY Projects/SBA.APIGateway/*.csproj ./Projects/SBA.APIGateway/

# Restore dependencies
RUN dotnet restore ./Projects/SBA.APIGateway/SBA.APIGateway.csproj --property:UseNuget=true

# Copy the rest of the source files
# COPY GLOB/GLOB.API.Config/ ./GLOB/GLOB.API.Config/
COPY Projects/SBA.APIGateway/ ./Projects/SBA.APIGateway/

# Build and publish
WORKDIR /app/Projects/SBA.APIGateway
RUN dotnet publish -c Release -o /app/out --property:UseNuget=true

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app/Projects/SBA.APIGateway

# Copy the published app from build stage
COPY --from=build-env /app/out .

# Runtime environment variables (can be overridden at runtime via CLI, Docker Compose, etc.)
ENV ASPNETCORE_URLS=http://+:1100
ENV DOTNET_ENVIRONMENT=Docker

EXPOSE 1100

ENTRYPOINT [ "dotnet", "SBA.APIGateway.dll" ]
