using GLOB.Infra.Data.Auth;


namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  // Dev (When Running Migration throw CLI)
  public static void SeedProjectz(this ModelBuilder mb)
  {
    "ModelBuilder --> Auth -> SeedProjectz".Print("EF Core");
  }
  // Prod (When Running Migration throw Automation)
  public static async Task SeedProjectz(this IApplicationBuilder app)
  {
    using (var srvcScp = app.ApplicationServices.CreateScope())
    { 
      var provider = srvcScp.ServiceProvider;
      DBCtxProjectz? context = provider.GetSrvc<DBCtxProjectz>();
      context.Database.EnsureCreated();
      "Auth -> EnsureCreated AppBuilder".Print("EF Core");
    }
  }

}