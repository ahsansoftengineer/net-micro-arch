
using Microsoft.Extensions.Configuration;

namespace GLOB.Infra.Data.Sqlz;
// No DI
public abstract partial class DBCtx : DbContext
{
  protected readonly IConfiguration _config;

  public string DOTNET_ENVIRONMENT { get; }
  public DBCtx(DbContextOptions options, IServiceProvider sp) : base(options)
  {
    _config = sp.GetSrvc<IConfiguration>();
    DOTNET_ENVIRONMENT = _config.GetValueStr("DOTNET_ENVIRONMENT");
  }

  // TODO: NOTE: Here we need to work for Seeding Data
  protected override void OnModelCreating(ModelBuilder mb)
  {

    EntityMappingConfig(mb);
    // OnModelCreatingEnumConfig(mb);
    // ConfigEnums(mb);
    mb.SeedInfra();
    base.OnModelCreating(mb);
  }
  
  // // SaveChanges Handle in UnitOfWork
  // public override int SaveChanges()
  // {
  //   var entries = ChangeTracker.Entries<EntityBeta>();
  //   foreach (var entry in entries)
  //   {
  //     if (entry.State == EntityState.Added)
  //     {
  //       entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
  //     }
  //     if (entry.State == EntityState.Modified)
  //     {
  //       entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
  //     }
  //   }
  //   return base.SaveChanges();
  // }

  // protected override void OnConfiguring(DbContextOptionsBuilder ob)
  // {
  //   ob.LogTo(Console.WriteLine);
  //   base.OnConfiguring(ob);
  // }
}
