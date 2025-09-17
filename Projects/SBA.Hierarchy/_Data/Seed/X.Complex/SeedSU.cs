using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedSU(this DBCtxProjectz context)
  {
    if (!context.SUs.Any(x => x.Id > 0))
    {
      context.SUs.AddRange(SeedDataSU<SU>());
      context.SaveChanges();
    }
  }
  public static void SeedSU(this ModelBuilder builder)
  {
    builder.Entity<SU>().HasData(SeedDataSU<SU>());
  }
 public static List<T> SeedDataSU<T>() where T : SU, new()
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
        OUId = i
      });
    }
    return list;
  }

}