

namespace GLOB.Infra.Data.Sqlz;

public class DBCtxInfra : DBCtx
{
  public DBCtxInfra(DbContextOptions<DBCtxInfra> options, IServiceProvider sp) : base(options, sp) { }
  protected override void OnModelCreating(ModelBuilder mb)
  {
    base.OnModelCreating(mb);
  }
}
