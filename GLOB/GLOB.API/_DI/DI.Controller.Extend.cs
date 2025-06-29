using GLOB.API.Clientz;
using GLOB.API.Config.DI;
using GLOB.Infra.Utils.MIddlewarez;

public static partial class DI_API
{


  public static void Add_API_Controller_Srvc_Extend(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Add_API_Config_Controller(config, (opt) =>
    {
      opt.Filters.Add<FilterCacheActionGet>();
      opt.Filters.Add<FilterCacheActionSave>();
      // opt.Filters.Add<FilterCacheActionGets>();
    });

    // srvc.AddSingleton<API_RabbitMQ>();

  }
}