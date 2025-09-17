

namespace GLOB.Infra.Data.Sqlz;
public abstract partial class DBCtx
{
  public DbSet<ProjectzLookupBase> ProjectzLookupBases { get; set; }
  public DbSet<ProjectzLookup> ProjectzLookups { get; set; }
  
}
