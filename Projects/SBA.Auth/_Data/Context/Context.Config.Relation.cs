

namespace SBA.Projectz.Data;
public partial class DBCtxProjectz
{
 
  private static void ConfigProjectzMapping(ModelBuilder mb)
  {
    mb.MapRefreshToken();
    // mb.Entity<Mapping_UserBusinessProfession>()
    //   .HasNoKey();
    // mb.Entity<Mapping_UserBusinessProfession>()
    //   .HasNoKey();
      // .HasKey(cs => new { cs.UserBusinessID, cs.IndustryID })
  }
}
