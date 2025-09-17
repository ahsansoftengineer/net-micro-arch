

namespace SBA.Projectz.Data;
public partial class DBCtxProjectz : DBCtx
{
  public DBCtxProjectz(DbContextOptions<DBCtxProjectz> options, IServiceProvider sp) : base(options, sp) { }
  protected override void OnModelCreating(ModelBuilder mb)
  {
    ConfigManyToOne(mb);
    // ConfigMicroServiceArch(mb);
    mb.SeedProjectz();

    base.OnModelCreating(mb);
  }
}
