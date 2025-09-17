## Dotnet Core CLI Commands
- Note: For Bash You can Revert BackSlash \ to ForwardSlash /
- Create a Folder for your project with that folder run the following commands

### SOLUTION COMMANDS
```bash
dotnet new sln -o SBA
```

### GLOB NEW
```bash
dotnet new classlib -o ./GLOB/GLOB.Extz

dotnet new classlib -o ./GLOB/GLOB.API.Config
dotnet new classlib -o ./GLOB/GLOB.Infra

dotnet new classlib -o ./GLOB/GLOB.API

dotnet new classlib -o ./GLOB/GLOB.Domain
```

### PROJECTZ NEW
```bash
dotnet new webapi -o ./Projects/SBA.APIGateway
dotnet new webapi -o ./Projects/SBA.Job
dotnet new webapi -o ./Projects/SBA.Auth

dotnet new webapi -o ./Projects/SBA.Hierarchy
dotnet new webapi -o ./Projects/SBA.Orderz
dotnet new webapi -o ./Projects/SBA.Userz

# dotnet new webapi -o SBA.Notify
```
### GLOB TO SOLUTION
```bash
dotnet sln add ./GLOB/GLOB.Extz/GLOB.Extz.csproj

dotnet sln add ./GLOB/GLOB.API.Config/GLOB.API.Config.csproj
dotnet sln add ./GLOB/GLOB.Infra/GLOB.Infra.csproj

dotnet sln add ./GLOB/GLOB.API/GLOB.API.csproj

dotnet sln add ./GLOB/GLOB.Domain/GLOB.Domain.csproj
```

### PROJECTZ TO SOLUTION
```bash
dotnet sln add ./Projects/SBA.APIGateway/SBA.APIGateway.csproj
dotnet sln add ./Projects/SBA.Job/SBA.Job.csproj
dotnet sln add ./Projects/SBA.Auth/SBA.Auth.csproj
dotnet sln add ./Projects/SBA.Hierarchy/SBA.Hierarchy.csproj
dotnet sln add ./Projects/SBA.Userz/SBA.Userz.csproj
dotnet sln add ./Projects/SBA.Orderz/SBA.Orderz.csproj
```

### GLOB RELATION
```bash
dotnet build
dotnet add ./GLOB/GLOB.API.Config/ reference ./GLOB/GLOB.Extz/ 
dotnet add ./GLOB/GLOB.Infra/ reference ./GLOB/GLOB.Extz/

dotnet add ./GLOB/GLOB.API/ reference ./GLOB/GLOB.Domain/
dotnet add ./GLOB/GLOB.API/ reference ./GLOB/GLOB.API.Config/
```
### Project Relation
```bash
dotnet add ./Projects/SBA.APIGateway/ reference ./GLOB/GLOB.API.Config/ # WEB API

dotnet add ./Projects/SBA.Auth/ reference ./GLOB/GLOB.API/ # WEB API
dotnet add ./Projects/SBA.Hierarchy/ reference ./GLOB/GLOB.API/ # WEB API
# dotnet remove ./Projects/SBA.Userz/ reference ./GLOB/GLOB.API/ # WEB API
# dotnet remove ./Projects/SBA.Orderz/ reference ./GLOB/GLOB.API/ # WEB API
```
### Local Packages
- Windows ||| C:\Users\Ahsan1008\.nuget\packages
- Ubuntu  ||| ~/.nuget/packages
- So, even if you use .NET 9, .NET 8, or .NET Core 3.1, all package versions are managed under:
```bash
# Create Dir
cd C:
mkdir Packages

# Create Source Dir

dotnet nuget add source 'C:\Packages\' --name Packages 
dotnet nuget list source

# Remove Source
dotnet nuget remove source Packages # Not Working

# Disable
dotnet nuget enable source nuget.org
dotnet nuget disable source nuget.org
dotnet nuget enable source "Microsoft Visual Studio Offline Packages"

```
### Using Local Package Source
```bash
dotnet add ./SBA.Auth/ package Microsoft.AspNetCore.Authentication.Google --version 9.0.0 --source "C:\Packages" # Worked
```

### Customize Package Location 

```bash
# Print File Location for Nuget Packges
dotnet nuget locals global-packages -l

# DEfault File Location for Nuget Packages
cd C:\Users\Ahsan1008\.nuget

# Custom File Location
setx NUGET_PACKAGES 'C:\Packages\'

```