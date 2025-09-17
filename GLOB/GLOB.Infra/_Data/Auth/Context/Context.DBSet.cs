using GLOB.Domain.Model.Auth;


namespace GLOB.Infra.Data.Auth;
public abstract partial class DBCtxIdentity
{
  public DbSet<ProjectzLookupBase> ProjectzLookupBases { get; set; }
  public DbSet<ProjectzLookup> ProjectzLookups { get; set; }
  public DbSet<RefreshToken> RefreshTokens { get; set; }
}
