

namespace SBA.Projectz.Data;
public partial class DBCtxProjectz
{
  private static void ConfigManyToOne(ModelBuilder mb)
  {
    // mb.Entity<Systemz>()
    //   .HasOne(e => e.Org)
    //   .WithMany(e => e.Systemzs)
    //   .HasForeignKey(e => e.OrgId)
    //   .IsRequired(false);
  }
}
