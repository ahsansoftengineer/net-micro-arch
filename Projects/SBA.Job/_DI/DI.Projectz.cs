using System.Threading.Tasks;
using GLOB.API.Clientz;
using Microsoft.EntityFrameworkCore.Migrations;
using SBA.Projectz.Mapper;
namespace SBA.Projectz.DI;

public static partial class DI_Projectz
{
  public static async Task Add_Projectz_Srvc(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Add_Projectz_Options(config);

    srvc.Add_API_Default_Srvc(config);
    srvc.Add_Projectz_Clientz(config);
    srvc.Add_Infra_Cache_Redis(config);
    srvc.Add_Infra_DB_SQL<DBCtxInfra, IUOW_Infra, UOW_Projectz>(config);
    srvc.Add_Infra_DB_SQL<DBCtxProjectz, IUOW_Projectz, UOW_Projectz>(config);

    // srvc.Add_API_Config_JWT_Option(); // Because of Identity
    srvc.AddAutoMapper(typeof(ProjectzMapper));
    srvc.Add_Hangfire(config);
    if (config.GetValueStr("DOTNET_ENVIRONMENT") != "Development")
      await srvc.MigrateNoValidate<DBCtxProjectz>();
  }
  public static void Add_Projectz_Clientz(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.AddSingleton<UOW_API_Httpz>();
    srvc.AddSingleton<MsgBusPub>();

  }
  
}