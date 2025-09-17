### ✅ Step-by-Step Safe Migration

1. ### **Generate Migration**

```bash
dotnet ef migrations add ChangeAgeType
```

2. ### **Edit the Migration (Up method)**

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    // 1. Rename original table to a temp name
    migrationBuilder.Sql("EXEC sp_rename 'Users', 'Users_Old'");

    // 2. Create new table with the updated schema
    migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new
        {
            Id = table.Column<int>(nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(nullable: true),
            Age = table.Column<string>(nullable: true) // now string
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Users", x => x.Id);
        });

    // 3. Migrate data from old to new table
    migrationBuilder.Sql(@"
        INSERT INTO Users (Id, Name, Age)
        SELECT Id, Name, CAST(Age AS VARCHAR(10)) FROM Users_Old
    ");

    // 4. Drop old table
    migrationBuilder.DropTable("Users_Old");
}
```

3. ### **Apply Migration**

```bash
dotnet ef database update
```

---

## 🧠 **Other Patterns (safer and less disruptive)**

### ✔️ **Use `RenameColumn` Instead of Drop+Add**

```csharp
migrationBuilder.RenameColumn("OldName", "Users", "NewName");
```

### ✔️ **Use `AlterColumn` Carefully**

Works for type/nullable changes that are safe:

```csharp
migrationBuilder.AlterColumn<string>(
    name: "Age",
    table: "Users",
    type: "nvarchar(10)",
    nullable: true,
    oldClrType: typeof(int),
    oldType: "int"
);
```

Use only when SQL Server can handle the conversion directly (e.g., int → string with data in a safe range).

---

## 🛡️ **Best Practices**

* **Always back up data** before running destructive migrations in production.
* Use **staging environments** to test migrations.
* Be careful with nullable → non-nullable changes: seed or fill default values.
* Prefer **manual migration customization** instead of relying fully on EF-generated logic when changing keys/types.

---

Let me know the **exact change you're planning**, and I can give you a **tailored migration** that preserves data safely.
