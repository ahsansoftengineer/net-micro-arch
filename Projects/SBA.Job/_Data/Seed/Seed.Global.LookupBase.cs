using GLOB.Hierarchy.Global;

namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  // For DB Context (Normal) (Prod Automate) 
  public static void SeedGlobalLookupBase(this DBCtxProjectz context)
  {
    if (!context.GlobalLookupBases.Any(x => x.Id > 0))
    {
      context.GlobalLookupBases.AddRange(SeedzInfra.SeedDataEntityBase<GlobalLookupBase>());
      context.SaveChanges();
    }
  }
  // For DB Context (Identity) (Dev CLI) 
  public static void SeedGlobalLookupBase(this ModelBuilder builder)
  {
    builder.Entity<GlobalLookupBase>().HasData(SeedzInfra.SeedDataEntityBase<GlobalLookupBase>());
  }

  
}