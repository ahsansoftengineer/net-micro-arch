

namespace GLOB.Infra.Data.Sqlz;
public static partial class SeedzInfra
{
  public static void SeedProjectzLookup(this DBCtxInfra context)
  {
    if (!context.ProjectzLookups.Any(x => x.Id > 0))
    {
      context.ProjectzLookups.AddRange(SeedDataProjectzLookup<ProjectzLookup>());
      context.SaveChanges();
    }
  }
  public static void SeedProjectzLookup(this ModelBuilder builder)
  {
    builder.Entity<ProjectzLookup>().HasData(SeedDataProjectzLookup<ProjectzLookup>());
  }
  public static List<T> SeedDataProjectzLookup<T>() where T : ProjectzLookup, new()
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
        ProjectzLookupBaseId = i
      });
    }
    return list;
  }
  
}