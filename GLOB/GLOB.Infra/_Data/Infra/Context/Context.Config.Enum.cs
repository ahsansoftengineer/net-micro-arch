

namespace GLOB.Infra.Data.Sqlz;
public abstract partial class DBCtx
{
  // Enums using Reflections to Store as String in DB
  protected virtual void OnModelCreatingEnumConfig(ModelBuilder mb)
  {
    foreach (var entityType in mb.Model.GetEntityTypes())
    {
      var properties = entityType.ClrType.GetProperties()
          .Where(p => p.Name == "Status" && p.PropertyType.IsEnum);

      foreach (var property in properties)
      {
        mb.Entity(entityType.ClrType)
          .Property(property.Name)
          .HasConversion<string>();
      }
    }
  }
  
  // Enum Setups
  
//   private static void ConfigEnums(ModelBuilder mb)
//   {
//     // Simple
//     AddStatusEnum<ProjectzEntityTest>(mb);
//     AddStatusEnum<Org>(mb);
   
//   }
//   private static void AddStatusEnum<TEntity>(ModelBuilder mb)
//     where TEntity : EntityBase
//   {
//     mb.Entity<TEntity>()
//       .Property(e => e.Status)
//       .HasConversion<string>();
//   }

}
