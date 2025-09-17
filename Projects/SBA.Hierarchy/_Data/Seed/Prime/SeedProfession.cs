using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedProfession(this DBCtxProjectz context)
  {
    if (!context.Professions.Any(x => x.Id > 0))
    {
      context.Professions.AddRange(SeedzInfra.SeedDataEntityBase<Profession>());
      context.SaveChanges();
    }
  }
  public static void SeedProfession(this ModelBuilder builder)
  {
    builder.Entity<Profession>().HasData(SeedzInfra.SeedDataEntityBase<Profession>());
  }
  

}