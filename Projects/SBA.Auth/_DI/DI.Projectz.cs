using SBA.Projectz.Mapper;
using SBA.Auth.Services;
using GLOB.API.Clientz;
using SBA.Projectz.Clientz;
using GLOB.Infra.Data.Auth;

namespace SBA.Projectz.DI;

public static partial class DI_Projectz
{
  public static void Add_Projectz_Srvc(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Add_Projectz_Options(config);

    //srvc.Add_API_Config_Localization(config);
    srvc.Add_API_Controller_Srvc_Extend(config);

    srvc.Add_API_Config_Authentication_JWT(config)
        .Config_Social_Auth(config);
    srvc.Add_API_Config_Authorization_JWT(config);

    srvc.Add_API_Config_Swagger(config);
    srvc.Add_API_Config_Cors_Auth();

    srvc.Add_Infra_Cache_Redis(config);

    srvc.Add_Infra_DB_SQL<DBCtxInfra, IUOW_Infra, UOW_Projectz>(config);
    srvc.Add_Infra_DB_SQL<DBCtxInfraIdentity, IUOW_Projectz, UOW_Projectz>(config);
    srvc.Add_Infra_DB_SQL_Identity<DBCtxProjectz, IUOW_Projectz, UOW_Projectz>(config);

    srvc.Add_API_Config_JWT_Option();
    // srvc.AddAuthorization();

    srvc.AddAutoMapper(typeof(ProjectzMapper));
    srvc.AddScoped<SmtpEmailSender>();
    srvc.AddScoped<TokenService>();
    
    srvc.AddSingleton<UOW_API_Httpz>();
    srvc.Add_API_RabbitMQ(config);
    
    srvc.AddHostedService<MsgBusSubs>(); // Jackson
  }
}
