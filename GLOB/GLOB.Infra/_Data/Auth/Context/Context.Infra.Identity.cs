

namespace GLOB.Infra.Data.Auth;
public class DBCtxInfraIdentity : DBCtxIdentity
{
  public DBCtxInfraIdentity(DbContextOptions<DBCtxInfraIdentity> options, IServiceProvider sp) : base(options, sp)
  {
  }

  protected override void OnModelCreating(ModelBuilder mb)
  {
    base.OnModelCreating(mb);
  }
}
