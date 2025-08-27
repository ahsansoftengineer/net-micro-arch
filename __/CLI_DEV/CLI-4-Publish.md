## Nuget Time Frame
- It takes 15 Minutes to publish
- Sometime U have to provide Version Full Version not 1.0.* To 1.0.63
## Add Publish Projects to Local Feeds
```bash
dotnet nuget add source "C:\Packages" --name Packages 
dotnet nuget remove source Packages
dotnet nuget list source

```

### PUBLISH Class Library (All)
- oy2admcfuhtm3ub5pnu5qghl2y6ykti2u5ovdy5dl55i66m

#### GLOB.Extz
```bash
dotnet build ./GLOB/GLOB.Extz/GLOB.Extz.csproj -c Release -p:PackageVersion=1.0.63
dotnet pack ./GLOB/GLOB.Extz/GLOB.Extz.csproj -c Release -p:PackageVersion=1.0.63
dotnet nuget push ./GLOB/GLOB.Extz/bin/Release/GLOB.Extz.1.0.63.nupkg --api-key oy2admcfuhtm3ub5pnu5qghl2y6ykti2u5ovdy5dl55i66m --source https://api.nuget.org/v3/index.json
cp ./GLOB/GLOB.Extz/bin/Release/GLOB.Extz.1.0.63.nupkg /c/Packages/
```

#### GLOB.API.Config (Specific)
- oy2abz6ry2ev5umiibufqs64izjruy5duppzurlosmyoxye
```bash
dotnet build ./GLOB/GLOB.API.Config/GLOB.API.Config.csproj -c Release -p:PackageVersion=1.0.63 -p:UseNuget=true
dotnet pack ./GLOB/GLOB.API.Config/GLOB.API.Config.csproj -c Release -p:PackageVersion=1.0.63 -p:UseNuget=true
dotnet nuget push ./GLOB/GLOB.API.Config/bin/Release/GLOB.API.Config.1.0.63.nupkg --api-key oy2admcfuhtm3ub5pnu5qghl2y6ykti2u5ovdy5dl55i66m --source https://api.nuget.org/v3/index.json
cp ./GLOB/GLOB.API.Config/bin/Release/GLOB.API.Config.1.0.63.nupkg /c/Packages/

```
#### GLOB.Infra
```bash
# dotnet restore ./GLOB/GLOB.API/GLOB.API.csproj --source C:\Packages
# dotnet build ./GLOB/GLOB.API/GLOB.API.csproj -c Release -p:UseNuget=false

dotnet build ./GLOB/GLOB.Infra/GLOB.Infra.csproj -c Release -p:PackageVersion=1.0.63 -p:UseNuget=true
dotnet pack ./GLOB/GLOB.Infra/GLOB.Infra.csproj -c Release -p:PackageVersion=1.0.63 -p:UseNuget=true
dotnet nuget push ./GLOB/GLOB.Infra/bin/Release/GLOB.Infra.1.0.63.nupkg --api-key oy2admcfuhtm3ub5pnu5qghl2y6ykti2u5ovdy5dl55i66m --source https://api.nuget.org/v3/index.json
cp ./GLOB/GLOB.Infra/bin/Release/GLOB.Infra.1.0.63.nupkg /c/Packages/

```
#### GLOB.API
```bash
# dotnet restore ./GLOB/GLOB.API/GLOB.API.csproj --source C:\Packages
dotnet build ./GLOB/GLOB.API/GLOB.API.csproj -c Release -p:PackageVersion=1.0.63 -p:UseNuget=true
dotnet pack ./GLOB/GLOB.API/GLOB.API.csproj -c Release -p:PackageVersion=1.0.63 -p:UseNuget=true
dotnet nuget push ./GLOB/GLOB.API/bin/Release/GLOB.API.1.0.63.nupkg --api-key oy2admcfuhtm3ub5pnu5qghl2y6ykti2u5ovdy5dl55i66m --source https://api.nuget.org/v3/index.json
cp ./GLOB/GLOB.API/bin/Release/GLOB.API.1.0.63.nupkg /c/Packages/

```
### Adding the Packages to Class Library
```bash
dotnet add ./GLOB/GLOB.API/GLOB.API.csproj package GLOB.API.Config -v 1.0.63

dotnet add ./GLOB/GLOB.Domain/GLOB.Domain.csproj package GLOB.Infra -v 1.0.63
```


### Adding the Packages to Projects
```bash
dotnet add ./Projects/SBA.APIGateway/SBA.APIGateway.csproj package GLOB.API.Config -v 1.0.63
dotnet add ./Projects/SBA.Job/SBA.Job.csproj package GLOB.API -v 1.0.63
dotnet add ./Projects/SBA.Auth/SBA.Auth.csproj package GLOB.API -v 1.0.63
dotnet add ./Projects/SBA.Hierarchy/SBA.Hierarchy.csproj package GLOB.API -v 1.0.63
```