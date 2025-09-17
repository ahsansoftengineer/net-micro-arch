

namespace SBA.Projectz.Data;
public static partial class SeedzProjectz
{
  // Dev (When Running Migration throw CLI)
  public static void SeedProjectz(this ModelBuilder mb)
  {
    "Hierarchy -> SeedProjectz".Print("ModelBuilder");
    // .-*
    mb.SeedGlobalLookupBase();
    mb.SeedOrg();
    mb.SeedBG();
    mb.SeedState();
    mb.SeedBank();
    mb.SeedBrand();
    mb.SeedIndustry();
    mb.SeedProfession();

    // *-.
    mb.SeedGlobalLookup();
    mb.SeedSystemz();
    mb.SeedLE();
    mb.SeedOU();
    mb.SeedSU();
    mb.SeedCity();

  }
  // Prod (When Running Migration throw Automation)
  public static void SeedProjectz(this IApplicationBuilder app)
  {
    using (var srvcScp = app.ApplicationServices.CreateScope())
    {
      DBCtxProjectz? context = srvcScp.ServiceProvider.GetSrvc<DBCtxProjectz>();
      "Hierarchy -> Applying Migrations AppBuilder".Print("EF Core");
      context.Database.EnsureCreated();
    }
  }

}