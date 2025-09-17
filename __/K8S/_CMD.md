
## PACKAGES INSTALLATION
```bash
dotnet new webapi -n PlatformService
dotnet add package AutoMapper.Extension.Microsoft.DependencyInjection --version 12
dotnet add package AutoMapper --version 12
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Microsoft.EntityFrameworkCore 
dotnet add package Microsoft.EntityFrameworkCore.Design 
dotnet add package Microsoft.EntityFrameworkCore.InMemory 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
```

### MIGRATIONS CMD
```bash
dotnet tool list --global
dotnet tool install --global dotnet-ef -v 8.0
Install-Package Microsoft.EntityFrameworkCore.Tools # Power Shell
dotnet ef migrations add NameOfMigration
dotnet ef database update 
detnet ef migrations remove
dotnet ef database drop --force
```

### Multi Projects CMD
```bash
# NOTE: --connection is not the part of add migrations
dotnet run --project SB_Admin.Web
dotnet ef migrations add NameOfMigration -p SB_Admin.Infra -s SB_Admin.Web --context DBCntx
dotnet ef database update 
dotnet ef database update -p SB_Admin.Infra -s SB_Admin.Web --connection "Server=.;Database=SB_Admin;User ID=sa;Password=P@55w0rd!123;TrustServerCertificate=True;"
dotnet ef migrations remove  -p SB_Admin.Infra -s SB_Admin.Web
dotnet ef database drop --force  -p SB_Admin.Infra -s SB_Admin.Web
# Trusted_Connection=True;Integrated Security=False;
```
### PACKAGE MANAGER CMD
```bash
UPDATE-DATABASE -Context DatabaseContext
Add-Migration NameOfMigration -Context DatabaseContextName
```

## K8S CMD
```bash
# Install the Below Package in Ubuntu
sudo snap install kubectl --classic
kubectl version

# Other Commands
kubectl get pods
kubectl get deployments
kubectl get services
kubectl get namespace
kubectl get storageclass
kubectl get pvc

# HARD RESTART DEPLOYMENTS
kubectl rollout restart deployments # Restart all deployments
kubectl rollout restart deployments name-depl # Restart 1 Deployments
kubectl rollout restart deployments name-depl1 name-depl2
```
