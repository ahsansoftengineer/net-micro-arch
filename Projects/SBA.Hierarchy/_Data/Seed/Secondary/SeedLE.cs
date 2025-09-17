using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedLE(this DBCtxProjectz context)
  {
    if (!context.LEs.Any(x => x.Id > 0))
    {
      context.LEs.AddRange(SeedDataLE<LE>());
      context.SaveChanges();
    }
  }
  public static void SeedLE(this ModelBuilder builder)
  {
    builder.Entity<LE>().HasData(SeedDataLE<LE>());
  }
  public static List<T> SeedDataLE<T>() where T : LE, new()
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
        BGId = i
      });
    }
    return list;
  }
}