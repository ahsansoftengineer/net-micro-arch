namespace GLOB.API.DI;
public static partial class DI_API
{
  public static void Add_API_Default_Srvc(this IServiceCollection srvc, IConfiguration config)
  {
    //srvc.Add_API_Config_Localization(config);
    srvc.Add_API_Config_Options(config);
    srvc.AddSingleton<HttpClient>();
    srvc.Add_API_Controller_Srvc_Extend(config);

    // srvc.Add_API_Config_Authentication_JWT(config);
    // srvc.Add_API_Config_Authorization_JWT(config);

    srvc.Add_API_Config_Swagger(config);
    srvc.Add_API_Config_Cors();
    // srvc.AddAutoMapper(typeof(API_Base_Mapper));

    // srvc.Add_API_Config_Versioning();
    // srvc.Add_API_Config_HttpCacheHeaders();
    // srvc.Add_API_Config_RateLimiting();
  }
}