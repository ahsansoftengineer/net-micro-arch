### COMMON
```bash
dotnet ef database update -p ./GLOB/GLOB.Infra -s SBA.Hierarchy --connection "Server=.;Database=Hierarchy;User Id=sa;Password=P@55w0rd!123;Encrypt=false;TrustServerCertificate=True;"

dotnet ef database drop --force -p ./GLOB/GLOB.Infra -s SBA.Hierarchy --context DBCtxInfra
dotnet ef migrations add Init -p ./GLOB/GLOB.Infra -s SBA.Hierarchy --context DBCtxInfra
dotnet ef migrations remove  -p ./GLOB/GLOB.Infra -s SBA.Hierarchz --context DBCtxInfra
# Below CMD NOT WORK USE launchSettings.json file
# DOTNET_ENVIRONMENT=Stage dotnet ef database update -s ./Projects/SBA.Auth --context DBCtxProjectz
```
### JOB
```bash
dotnet ef database drop --force -s ./Projects/SBA.Job --context DBCtxProjectz
dotnet ef migrations add Init6 -s ./Projects/SBA.Job --context DBCtxProjectz
dotnet ef database update -s ./Projects/SBA.Job --context DBCtxProjectz
dotnet ef migrations remove -s ./Projects/SBA.Job --context DBCtxProjectz
```
### HIERARCHY
```bash
dotnet ef database drop --force -s ./Projects/SBA.Hierarchy --context DBCtxProjectz
dotnet ef migrations add Init -s ./Projects/SBA.Hierarchy --context DBCtxProjectz
dotnet ef database update -s ./Projects/SBA.Hierarchy --context DBCtxProjectz
dotnet ef migrations remove -s ./Projects/SBA.Hierarcy --context DBCtxProjectz
```
### AUTH
```bash
dotnet ef database drop --force -s ./Projects/SBA.Auth --context DBCtxProjectz
dotnet ef migrations add Init -s ./Projects/SBA.Auth --context DBCtxProjectz
dotnet ef database update -s ./Projects/SBA.Auth --context DBCtxProjectz
dotnet ef migrations remove -s ./Projects/SBA.Auth --context DBCtxProjectz

```

### USERSZ
```bash

```
### ORDERZ
```bash

```

## Complex Table Migration Structure

✅ **1. Start with Independent (Root) Tables**

* Create tables that **don’t depend on others** (no foreign keys yet).
* Typically, these are **lookup tables** or **aggregate roots** in DDD.
* Example: `Users`, `Roles`, `Permissions`.

✅ **2. Add Child Tables with Foreign Keys**

* Next, create tables that reference **already-existing parent tables**.
* Example: `UserProfiles` referencing `Users`, `Orders` referencing `Customers`.

✅ **3. Add Many-to-Many Join Tables**

* Create **junction/bridge tables** for many-to-many relationships **after both sides exist**.
* Example: `UserRoles` (needs both `Users` and `Roles` first).

✅ **4. Add Optional / Complex Relationships**

* For self-referencing or circular relationships, **split into two migrations**:

  1. Create the table without the FK
  2. Add FK in a separate migration once the table exists

✅ **5. Add Constraints, Indexes, and Defaults**

* Add **unique constraints, indexes, computed columns** after the base schema is stable.
* This avoids migration errors due to missing columns.

✅ **6. Handle Seed Data Last**

* Seed **lookup data** (like enums, default roles) only **after all required tables are in place**.

---

### TL;DR Recommended Migration Flow

1️⃣ **Base entities without relationships**
2️⃣ **Dependent entities with simple FKs**
3️⃣ **Many-to-many join tables**
4️⃣ **Complex/self-referencing FKs (separate migration if needed)**
5️⃣ **Constraints, indexes, computed columns**
6️⃣ **Seed data**

Would you like me to **show an example migration sequence** for a complex case (e.g., `Users → Roles → Permissions → UserRoles`)?
