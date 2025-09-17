using GLOB.API.Config.Optionz;
using Microsoft.Extensions.Options;

namespace SBA.Projectz.DI;
public static partial class DI_Projectz
{
  // http://localhost:1106/api/Gateway/v1/swagger/index.html
  // http://localhost:1106/api/Gateway/v1/swagger/v1/swagger.json
  public static void Add_Swagger_Gateway(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Configure<Option_SwaggerServices>(config.GetSection(Option_SwaggerServices.SectionName));
    srvc.Add_API_Config_Swagger(config);
  }

  public static void Use_Swagger_Gateway(this IApplicationBuilder app)
  {
    app.Use_API_Config_Swagger((c) => {
      var Option = app.GetSrvc<IOptions<Option_SwaggerServices>>();
      foreach (var service in Option?.Value?.Services)
      {
        (service.Name + "  --  " + service.Url).Print("Swagger");
        c.SwaggerEndpoint(service.Url, service.Name);
      }
    });
  }
}