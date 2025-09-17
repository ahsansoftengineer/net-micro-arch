using GLOB.Domain.Hierarchy;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  public static void SeedState(this DBCtxProjectz context)
  {
    if (!context.States.Any(x => x.Id > 0))
    {
      context.States.AddRange(SeedzInfra.SeedDataEntityBase<State>());
      context.SaveChanges();
    }
  }
  public static void SeedState(this ModelBuilder builder)
  {
    builder.Entity<State>().HasData(SeedzInfra.SeedDataEntityBase<State>());
  }
  

}