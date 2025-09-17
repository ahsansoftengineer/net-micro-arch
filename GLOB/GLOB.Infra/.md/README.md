### Parent API Docs
- Note: Servicies are required when not in Micro Arch
- Current IOC Containers of Child Are being Used

### Migrations
```bash
dotnet ef database update -p GLOB.Infra -s SBA.Hierarchy --connection "Server=localhost;Database=SBA;User Id=sa;Password=asdf1234;Encrypt=false"
dotnet ef migrations add Init -s SBA.Hierarchy --context ProjectzDBCntxt
dotnet ef migrations remove -s SBA.Hierarchy
dotnet ef database update -s SBA.Hierarchy
dotnet run --project SBA.Hierarchy
```