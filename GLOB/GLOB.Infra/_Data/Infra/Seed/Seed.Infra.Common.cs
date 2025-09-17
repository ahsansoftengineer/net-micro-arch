
namespace GLOB.Infra.Data.Sqlz;
public static partial class SeedzInfra
{
  // // For DB Context (Normal) (Prod Automate) 
  // public static void SeedTestInfra(this DBCtxInfra context)
  // {
  //   if (!context.TestInfras.Any(x => x.Id > 0))
  //   {
  //     context.TestInfras.AddRange(SeedDataEntityBase<API_Infra_EntityTest>());
  //     context.SaveChanges();
  //   }
  // }
  // // For DB Context (Identity) (Prod Automate)
  // public static void SeedTestInfra(this DBCtxInfraIdentity context)
  // {
  //   if (!context.TestInfras.Any(x => x.Id > 0))
  //   {
  //     context.TestInfras.AddRange(SeedDataEntityBase<API_Infra_EntityTest>());
  //     context.SaveChanges();
  //   }
  // }
  // // For DB Context (Identity) (Dev CLI) 
  // public static void SeedTestInfra(this ModelBuilder builder)
  // {
  //   builder.Entity<API_Infra_EntityTest>().HasData(SeedDataEntityBase<API_Infra_EntityTest>());
  // }
  public static List<T> SeedDataEntityBase<T>() where T : EntityBase, new()
  {
    string className = typeof(T).Name;
    List<T> list = new List<T>();
    for (int i = 1; i <= 3; i++)
    {
      list.Add(new T()
      {
        Id = i,
        Name = $"{className} {i}",
        Desc = $"{className} {i} Desc",
        Status = Status.None,
        IsSelected = false
        
      });
    }
    return list;
  }
}