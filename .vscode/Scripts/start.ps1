# PS C:\D\sba-micro> .\.vscode\all-run.ps1

Start-Process powershell -ArgumentList '-NoExit', '-Command', 'dotnet watch --launch-profile https --no-build --debug --project ../SBA.APIGateway/SBA.APIGateway.csproj' -WorkingDirectory "$PSScriptRoot"
Start-Process powershell -ArgumentList '-NoExit', '-Command', 'dotnet watch --launch-profile https --no-build --debug --project ../SBA.Auth/SBA.Auth.csproj' -WorkingDirectory "$PSScriptRoot"
Start-Process powershell -ArgumentList '-NoExit', '-Command', 'dotnet watch --launch-profile https --no-build --debug --project ../SBA.Hierarchy/SBA.Hierarchy.csproj' -WorkingDirectory "$PSScriptRoot"
