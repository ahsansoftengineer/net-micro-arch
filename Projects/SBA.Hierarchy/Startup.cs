
namespace SBA.Hierarchy;
public class Startup
{
  private IConfiguration _config { get; }

  public Startup(IConfiguration config)
  {
    _config = config;
  }

  public void ConfigureServices(IServiceCollection srvc)
  {
    srvc.Add_Projectz_Srvc(_config);
  }
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    app.Use_API_Default_Middlewares();

    app.SeedProjectz();
  }
}