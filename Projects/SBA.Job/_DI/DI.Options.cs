namespace SBA.Projectz.DI;
public static partial class DI_Projectz
{
  public static void Add_Projectz_Options(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Add_API_Config_Options(config);
    srvc.Configure<Option_Hangfire>(config);
    
  }
}