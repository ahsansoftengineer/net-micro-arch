using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedSystemz(this DBCtxProjectz context)
  {
    if (!context.Systemzs.Any(x => x.Id > 0))
    {
      context.Systemzs.AddRange(SeedDataSystemz<Systemz>());
      context.SaveChanges();
    }
  }
  public static void SeedSystemz(this ModelBuilder builder)
  {
    builder.Entity<Systemz>().HasData(SeedDataSystemz<Systemz>());
  }
  public static List<T> SeedDataSystemz<T>() where T : Systemz, new()
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
        OrgId = i
      });
    }
    return list;
  }

}