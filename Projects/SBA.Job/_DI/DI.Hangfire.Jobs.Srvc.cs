using SBA.Projectz.Srvc;

namespace SBA.Projectz.DI;
public static partial class DI_Projectz
{
  public static void Add_Hangfire_Srvcs(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.AddTransient<SrvcInfo>();
    srvc.AddTransient<SrvcProjectzLookup>();
  }
}