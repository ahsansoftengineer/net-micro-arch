using GLOB.Infra.Utils.MIddlewarez;
using GLOB.Infra.Data.Redisz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GLOB.Infra.DI;

public static partial class DI_Infra
{
  public static void Add_Infra_Cache_Redis(this IServiceCollection srvc, IConfiguration config)
  {
    string conn = config.GetConnectionString("Redis");
    srvc.AddStackExchangeRedisCache(options =>
    {
      options.Configuration = conn;
    });

    srvc.AddSingleton<IConnectionMultiplexer>(sp =>
      ConnectionMultiplexer.Connect(conn));
    srvc.AddSingleton<RedisCacheService>();

    srvc.AddSingleton<FilterCacheActionGet>();
    srvc.AddSingleton<FilterCacheActionSave>();
    // srvc.AddSingleton<FilterCacheActionGets>();
  }
}

