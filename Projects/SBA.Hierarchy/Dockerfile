# ===========================================================
# Build:
# docker build -t mansanhhub/sba-srvc-hierarchy -f Projects/SBA.Hierarchy/Dockerfile .
# docker push mansanhhub/sba-srvc-hierarchy
# docker run -d --name sba-srvc-hierarchy -p 1106:1106 -e DOTNET_ENVIRONMENT=Docker mansanhhub/sba-srvc-hierarchy
# docker rm -f sba-srvc-hierarchy
# docker exec -it <container_id> /bin/bash

# http://localhost:1106/api/hierarchy/v1/swagger/index.html
# http://localhost:1106/api/hierarchy/v1/_projectz-lookup/gets-lookup

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy only .csproj files first (to enable Docker caching of restore)
# COPY GLOB/GLOB.API.Config/*.csproj ./GLOB/GLOB.API.Config/
# COPY GLOB/GLOB.Infra/*.csproj ./GLOB/GLOB.Infra/
# COPY GLOB/GLOB.API/*.csproj ./GLOB/GLOB.API/

COPY GLOB/GLOB.Domain/*.csproj ./GLOB/GLOB.Domain/
COPY Projects/SBA.Hierarchy/*.csproj ./Projects/SBA.Hierarchy/

# Restore dependencies
RUN dotnet restore ./Projects/SBA.Hierarchy/SBA.Hierarchy.csproj --property:UseNuget=true

# Copy the rest of the source files
# COPY GLOB/GLOB.API.Config/ ./GLOB/GLOB.API.Config/
# COPY GLOB/GLOB.Infra/ ./GLOB/GLOB.Infra/
# COPY GLOB/GLOB.API/ ./GLOB/GLOB.API/

COPY GLOB/GLOB.Domain/ ./GLOB/GLOB.Domain/
COPY Projects/SBA.Hierarchy/ ./Projects/SBA.Hierarchy/

# Build and publish
WORKDIR /app/Projects/SBA.Hierarchy
RUN dotnet publish -c Release -o /app/out --property:UseNuget=true

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app/Projects/SBA.Hierarchy

# Copy the published app from build stage
COPY --from=build-env /app/out .

# Runtime environment variables (can be overridden at runtime via CLI, Docker Compose, etc.)
ENV ASPNETCORE_URLS=http://+:1106
ENV DOTNET_ENVIRONMENT=Docker
ENV ConnectionStrings__SqlConnection="Server=host.docker.internal,1433;Initial Catalog=SBA_Hierarchy;User ID=sa;Password=P@55w0rd!123;TrustServerCertificate=true;"

# Cannot do this
# Need Network for this
# ENV API_Httpz__Auth=http://srvc-auth:1104/api/auth/v1

EXPOSE 1106

ENTRYPOINT [ "dotnet", "SBA.Hierarchy.dll" ]