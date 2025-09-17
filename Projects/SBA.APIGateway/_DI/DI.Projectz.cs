
namespace SBA.Projectz.DI;
public static partial class DI_Projectz
{

  public static void Add_Projectz_Srvc(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Add_Projectz_Options(config);
    // Add_API_Config_Caching(srvc);
    // srvc.AddAuthentication();
    // srvc.AddAuthorization();
    srvc.Add_API_Config_Cors_Gateway();
    srvc.Add_Swagger_Gateway(config);
    srvc.Add_API_Config_Controller(config);
    // srvc.Add_API_Config_Versioning();
  }
  public static void Add_Projectz_Srvc2(this IServiceCollection srvc)
  {
    // srvc.Config_Identity();
    // srvc.Add_API_Config_Versioning();
    // srvc.Add_API_Config_HttpCacheHeaders();
    // srvc.Add_API_Config_RateLimiting();
    // srvc.Add_API_Config_StaticFiles();
  }
}