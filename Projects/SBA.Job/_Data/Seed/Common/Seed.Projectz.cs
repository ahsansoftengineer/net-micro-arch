

using LinqKit;

namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  // Dev (When Running Migration throw CLI)
  public static void SeedProjectz(this ModelBuilder mb)
  {
    "ModelBuilder --> Job -> SeedProjectz".Print("EF Core");
    // .-*
    mb.SeedGlobalLookupBase();
    mb.SeedGlobalLookup();
    // *-.

  }
  // Prod (When Running Migration throw Automation)
  // public static void SeedProjectz(this IApplicationBuilder app)
  // {
  //   using var srvcScp = app.ApplicationServices.CreateScope();
  //   DBCtxProjectz? context = srvcScp.ServiceProvider.GetSrvc<DBCtxProjectz>();
  //   "Job -> Applying Migrations AppBuilder".Print("EF Core");
  //   context.Database.GetMigrations().ToList().ForEach(x =>
  //   {
  //     x.Print("Migrations");
  //   });
  //   if (context.Database.HasPendingModelChanges())
  //   {
  //     context.Database.EnsureCreated();
  //   }
  //   else
  //   {
  //     context.Database.Migrate();
  //   }
  // }

}