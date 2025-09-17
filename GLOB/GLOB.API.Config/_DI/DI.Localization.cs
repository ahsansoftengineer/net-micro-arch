
namespace GLOB.API.Config.DI;

public static partial class DI_API_Config
{
  public static void Add_API_Config_Localization(this IServiceCollection srvc, IConfiguration config)
  {
    string path = config.GetValueStr("ASPNET_Resources"); //"Resources";
    srvc.AddLocalization(options =>
    {
      // Specify where your .resx files are located
      options.ResourcesPath = path;
    });
  }
  public static void Use_API_Config_Localization(this IApplicationBuilder app)
  {
    IConfiguration config = app.GetSrvc<IConfiguration>();
    string supportedCulture = config.GetValueStr("ASPNET_Localization"); //"en,ur";
    string[] supportedCultures = supportedCulture.Split(",");

    var localizationOptions = new RequestLocalizationOptions()
      .SetDefaultCulture("en")
      .AddSupportedCultures(supportedCultures)
      .AddSupportedUICultures(supportedCultures);

    app.UseRequestLocalization(localizationOptions);
  }
}