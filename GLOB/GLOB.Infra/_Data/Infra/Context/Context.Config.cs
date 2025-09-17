

namespace GLOB.Infra.Data.Sqlz;
public abstract partial class DBCtx
{
  public static void EntityMappingConfig(ModelBuilder mb)
  {
    ConfigOneToMany(mb);
  }

  public static void ConfigOneToMany(ModelBuilder mb)
  {
    mb.Entity<ProjectzLookup>()
      .HasOne(p => p.ProjectzLookupBase)
      .WithMany(b => b.ProjectzLookups) // assumes navigation collection exists
      .HasForeignKey(p => p.ProjectzLookupBaseId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}