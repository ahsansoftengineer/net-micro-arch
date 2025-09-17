using GLOB.Infra.Data.Auth;
using SBA.Auth.Services;

namespace SBA.Projectz.DI;
public static partial class DI_Projectz
{
  public static void Add_Projectz_Options(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.Add_API_Config_Options(config);
    srvc.Configure<Option_JwtSettings>(config.GetSection(Option_JwtSettings.SectionName));
    srvc.Configure<Option_EmailSettings>(config.GetSection(Option_EmailSettings.SectionName));
    srvc.Configure<Option_Identity>(config.GetSection(Option_Identity.SectionName));
    srvc.Configure<Option_SocialAccounts>(config.GetSection(Option_SocialAccounts.SectionName));
  }
}
