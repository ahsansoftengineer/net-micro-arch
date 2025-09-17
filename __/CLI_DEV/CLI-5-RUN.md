### RUNNING PROJECTS
- Change launch.json profile        : --launch-profile http
- Change Project Ref csproj         : --property:UseNuget=true
- Run ProjectName                   : --project ./Projects/SBA.APIGateway/

```bash
                                                                                                                     # http, https
dotnet watch run --property:UseNuget=true --launch-profile http --project ./Projects/SBA.APIGateway/     # 1100 1101
dotnet watch run --property:UseNuget=true --launch-profile http --project ./Projects/SBA.Auth/           # 1104 1105
dotnet watch run --property:UseNuget=true --launch-profile http --project ./Projects/SBA.Hierarchy/      # 1106 1107
dotnet watch run --property:UseNuget=true --launch-profile http --project ./Projects/SBA.Job/            # 1102 1103
dotnet watch run --property:UseNuget=true --launch-profile http --project ./Projects/SBA.Userz/          # 1108 1109
dotnet watch run --property:UseNuget=true --launch-profile http --project ./Projects/SBA.Orderz/         # 1110 1111
```

dotnet watch  
#### USER SECRETS
```bash 
dotnet user-secrets init --project ./SBA.Api/
dotnet user-secrets set --project ./SBA.Api/ "Option_JwtSettings:Secret" "super-secret-key-from-user-secrets"
dotnet user-secrets list --project ./SBA.Api/
```

### GIT
```bash
start . # OPENING FOLDER EXPLORER USING CLI

dotnet new gitignore
git init
git push --set-upstream origin BranchNameHere
git remote set-url stream https://gitlab.com/starbazaar/admin-panel.git
git remote add stream https://gitlab.com/starbazaar/webapp.git
git remote -v
origin  https://gitlab.com/m.ahsan.saifi/webapp.git (fetch)
origin  https://gitlab.com/m.ahsan.saifi/webapp.git (push)
stream  https://gitlab.com/starbazaar/webapp.git (fetch)
stream  https://gitlab.com/starbazaar/webapp.git (push)
dotnet new editorconfig
```
