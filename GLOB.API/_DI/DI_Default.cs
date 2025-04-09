using GLOB.API.Mapper;
using GLOB.API.Middlewarez;

namespace GLOB.API.DI;
public static partial class API_DI_Common
{
  public static void Add_API_DI_Common(this IServiceCollection srvc, string ProjectNameSwagger = "Swagger Name Project")
  {
    // Config_CachingService(srvc);
    // srvc.AddAuthentication();
    // srvc.AddAuthorization();
    srvc.Config_Cors();
    srvc.AddAutoMapper(typeof(API_Base_Mapper));
    srvc.Config_Controllerz();
    srvc.Config_Swagger(ProjectNameSwagger);
    // srvc.Config_Versioning();
  }
  public static void Add_API_DefaultExternalServices(this IServiceCollection srvc)
  {
    // srvc.Config_Identity();
    srvc.Config_Versioning();
    srvc.Config_HttpCacheHeaders();
    srvc.Config_RateLimiting();
    // srvc.Config_FileHandling();
  }
  public static void AddDefaultExternalConfiguration(this IApplicationBuilder app,
    IWebHostEnvironment env)
  {
    // app.Config_StaticFilesHandling();
    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.Config_DevEnv(env);
    app.Config_ExceptionHandler();
    app.UseHttpsRedirection();

    app.UseCors("CorsPolicyAllowAll");
    // app.Config_Caching();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.Config_Controller();
  }
  
}