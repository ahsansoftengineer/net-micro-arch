using GLOB.API.Clientz;
using SBA.Projectz.Clientz;

namespace SBA.Projectz.DI;

public static partial class DI_Projectz
{
  public static void Add_Projectz_Clientz(this IServiceCollection srvc)
  {
    srvc.AddSingleton<UOW_API_Httpz>();
    srvc.Add_Projectz_RMQ();
  }
  public static void Add_Projectz_RMQ(this IServiceCollection srvc)
  {
    // srvc.AddSingleton<MsgBusPub>();
    // srvc.AddSingleton<Projectz_RMQ_Pub>();
    srvc.AddSingleton<Projectz_RMQ_Sub>();
    srvc.AddHostedService<MsgBusSubs>(); // Jackson
    srvc.AddHostedService<RMQ_Sub_Lookup_Create>();
    srvc.AddHostedService<RMQ_Sub_Lookup_Delete>();
    srvc.AddHostedService<RMQ_Sub_Lookup_Status>();
    srvc.AddHostedService<RMQ_Sub_Lookup_Update>();
  }
}
