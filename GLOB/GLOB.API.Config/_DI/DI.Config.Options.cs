
namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static void Add_API_Config_Options(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Configure<Option_App>(config);
  }
}