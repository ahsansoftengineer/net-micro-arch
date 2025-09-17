using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedBank(this DBCtxProjectz context)
  {
    if (!context.Banks.Any(x => x.Id > 0))
    {
      context.Banks.AddRange(SeedzInfra.SeedDataEntityBase<Bank>());
      context.SaveChanges();
    }
  }
  public static void SeedBank(this ModelBuilder builder)
  {
    builder.Entity<Bank>().HasData(SeedzInfra.SeedDataEntityBase<Bank>());
  }
  

}