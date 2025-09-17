using GLOB.Hierarchy.Global;

namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedGlobalLookup(this DBCtxProjectz context)
  {
    if (!context.GlobalLookups.Any(x => x.Id > 0))
    {
      context.GlobalLookups.AddRange(SeedDataGlobalLookup<GlobalLookup>());
      context.SaveChanges();
    }
  }
  public static void SeedGlobalLookup(this ModelBuilder builder)
  {
    builder.Entity<GlobalLookup>().HasData(SeedDataGlobalLookup<GlobalLookup>());
  }
  public static List<T> SeedDataGlobalLookup<T>() where T : GlobalLookup, new()
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
        Code = $"{i}{i}{i}-{i}{i}{i}-{i}{i}{i}",
        GlobalLookupBaseId = i
      });
    }
    return list;
  }
  
}