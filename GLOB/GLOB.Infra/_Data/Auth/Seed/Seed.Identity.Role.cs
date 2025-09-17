using GLOB.Domain.Model.Auth;
using Microsoft.AspNetCore.Identity;


namespace GLOB.Infra.Data.Auth;
public static partial class InfraSeedIdentity
{
  public static void SeedInfraRole(this ModelBuilder mb)
  {
    mb.Entity<InfraRole>().HasData(SeedDataRole<InfraRole>());
  }
  public static async Task SeedInfraRole(this RoleManager<InfraRole> mngr)
  {
    if (!await mngr.Roles.AnyAsync())
    {
      foreach (var item in SeedDataRole<InfraRole>())
      {
        await mngr.CreateAsync(item);
      }
    }
  }
  // public static async Task SeedRoleInfra(this DBCtxInfraIdentity context)
  // {
  //   if (!context.Roles.Any())
  //   {
  //     await context.Roles.Add(SeedDataRole<InfraRole>());
  //     context.SaveChanges();
  //   }
  // }
  public static List<T> SeedDataRole<T>() where T : InfraRole, new()
  //  IdentityRole<string>
  {
    string className = typeof(T).Name;
    List<T> list = new List<T>();
    var guid = new []
    {
      "b4206884-fc69-4a1b-a4ca-81f4cf594ee5",
      "832f9537-20c4-49ca-9f12-b8c5f9515c17",
      "8c40418c-4ac7-4f2e-9def-8ceeb5f5c556"
    };

    for (int i = 1; i <= 3; i++)
    {
      string name = $"{className}_{i}";
      string NAME = name.ToUpperInvariant(); // ToUpper is not Recommended
      var data = new T()
      {

        Id = guid[i-1],
        Name = name,
        NormalizedName = NAME
      };
      list.Add(data);
    }
    return list;
  }
}