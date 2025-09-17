using AspNetCoreRateLimit;

namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static void Add_API_Config_Versioning(this IServiceCollection srvc)
  {
    srvc.AddApiVersioning(opt =>
    {
      opt.ReportApiVersions = true;
      opt.AssumeDefaultVersionWhenUnspecified = true;
      opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    });
  }
  // API Throttling 2: 
  public static void Add_API_Config_RateLimiting(this IServiceCollection srvc)
  {
    var rateLimitRules = new List<RateLimitRule>
    {
    new RateLimitRule
    {
      Endpoint = "*",
      Limit = 10,
      Period = "1m"
    }
    };

    srvc.Configure<IpRateLimitOptions>(opt =>
    {
      opt.GeneralRules = rateLimitRules;
    });
    srvc.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    srvc.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    srvc.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
  }
}