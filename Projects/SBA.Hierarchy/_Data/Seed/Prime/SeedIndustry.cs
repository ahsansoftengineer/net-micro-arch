using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedIndustry(this DBCtxProjectz context)
  {
    if (!context.Industrys.Any(x => x.Id > 0))
    {
      context.Industrys.AddRange(SeedzInfra.SeedDataEntityBase<Industry>());
      context.SaveChanges();
    }
  }
  public static void SeedIndustry(this ModelBuilder builder)
  {
    builder.Entity<Industry>().HasData(SeedzInfra.SeedDataEntityBase<Industry>());
  }
  

}