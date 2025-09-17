
namespace SBA.Projectz.DI;
public static partial class DI_Projectz
{
  private static void Use_API_Gateway_DevEnv(this IApplicationBuilder app)
  {
    var env = app.GetSrvc<IWebHostEnvironment>();
    $"Current Environment: {env.EnvironmentName}".Print();
    if (!env.IsProduction())
    {
      app.UseDeveloperExceptionPage();
      app.Use_Swagger_Gateway();
    }
  }
  public static void Use_API_Default_Middlewares(this IApplicationBuilder app)
  {
    // app.UseMiddleware<GlobalExceptionMiddleware>();
    // app.Use_API_Config_ExceptionHandler();
    app.Use_API_Gateway_DevEnv();
    // app.UseHttpsRedirection();
    // app.Use_API_Config_Caching();
    app.UseRouting();
    // app.UseCors("AllowGateway");
    // app.UseAuthentication();
    // app.UseAuthorization();
    app.Use_API_Config_Controller();
  }

}