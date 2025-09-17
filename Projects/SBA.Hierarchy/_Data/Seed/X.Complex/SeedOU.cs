using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedOU(this DBCtxProjectz context)
  {
    if (!context.OUs.Any(x => x.Id > 0))
    {
      context.OUs.AddRange(SeedDataOU<OU>());
      context.SaveChanges();
    }
  }
  public static void SeedOU(this ModelBuilder builder)
  {
    builder.Entity<OU>().HasData(SeedDataOU<OU>());
  }
  public static List<T> SeedDataOU<T>() where T : OU, new()
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
        LEId = i,
        Law = className + "Law",
        Address = className + "Address",
        Deposit = className + "Deposit",
        LogoImg = className + "LogoImg",
        TopImg = className + "TopImg",
        WarningImg = className + "WarningImg",
        FooterImg = className + "FooterImg"
      });
    }
    return list;
  }
}