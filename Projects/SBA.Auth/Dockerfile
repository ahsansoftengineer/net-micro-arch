# ===========================================================
# Build:
# docker build -t mansanhhub/sba-srvc-auth -f Projects/SBA.Auth/Dockerfile .
# docker push mansanhhub/sba-srvc-auth
# docker run -d --name sba-srvc-auth -p 1104:1104 -e DOTNET_ENVIRONMENT=Docker mansanhhub/sba-srvc-auth
# docker rm -f sba-srvc-auth
# docker exec -it <container_id> /bin/bash

# http://localhost:1104/api/auth/v1/swagger/index.html
# http://localhost:1104/api/auth/v1/_projectz-lookup/gets-lookup
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy only .csproj files first (to enable Docker caching of restore)
# COPY GLOB/GLOB.API.Config/*.csproj ./GLOB/GLOB.API.Config/
# COPY GLOB/GLOB.Infra/*.csproj ./GLOB/GLOB.Infra/
# COPY GLOB/GLOB.API/*.csproj ./GLOB/GLOB.API/

COPY GLOB/GLOB.Domain/*.csproj ./GLOB/GLOB.Domain/
COPY Projects/SBA.Auth/*.csproj ./Projects/SBA.Auth/

# Restore dependencies
RUN dotnet restore ./Projects/SBA.Auth/SBA.Auth.csproj --property:UseNuget=true

# Copy the rest of the source files
# COPY GLOB/GLOB.API.Config/ ./GLOB/GLOB.API.Config/
# COPY GLOB/GLOB.Infra/ ./GLOB/GLOB.Infra/
# COPY GLOB/GLOB.API/ ./GLOB/GLOB.API/

COPY GLOB/GLOB.Domain/ ./GLOB/GLOB.Domain/
COPY Projects/SBA.Auth/ ./Projects/SBA.Auth/

# Build and publish
WORKDIR /app/Projects/SBA.Auth
# RUN dotnet publish -c Release -o /app/out --property:UseNuget=true
RUN dotnet publish -c Release -o /app/out --property:UseNuget=true

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app/Projects/SBA.Auth

# Copy the published app from build stage
COPY --from=build-env /app/out .

# Runtime environment variables (can be overridden at runtime via CLI, Docker Compose, etc.)
ENV ASPNETCORE_URLS=http://+:1104
ENV DOTNET_ENVIRONMENT=Docker
ENV ConnectionStrings__SqlConnection="Server=host.docker.internal,1433;Initial Catalog=SBA_Auth;User ID=sa;Password=P@55w0rd!123;TrustServerCertificate=true;"

EXPOSE 1104

ENTRYPOINT [ "dotnet", "SBA.Auth.dll" ]