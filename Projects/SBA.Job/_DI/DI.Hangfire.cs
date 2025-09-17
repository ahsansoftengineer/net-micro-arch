using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Storage.SQLite;
using HangfireBasicAuthenticationFilter;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace SBA.Projectz.DI;

public static partial class DI_Projectz
{
  public static void Add_Hangfire(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.AddHangfire(opt =>
    {
      opt
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings(opt =>
        {

        });
      if (config.GetValueStr("DOTNET_ENVIRONMENT") == "Development" && false)
      {
        opt.UseSQLiteStorage(Path.Combine("../../../", config.GetConnectionString("SQLite")), new SQLiteStorageOptions
        {
          QueuePollInterval = TimeSpan.FromSeconds(15),
          JobExpirationCheckInterval = TimeSpan.FromHours(1),
          CountersAggregateInterval = TimeSpan.FromMinutes(5),
        });
      }
      else
      {
        // Danger: Hangfire Seperate DB is Required for Inital Migrations
        // Note: Otherwise you dont have Projectz Migration Run
        var seperateDB = config.GetConnectionString("SqlConnectionHangfire");
        seperateDB.EnsureDatabaseExists();
        opt.UseSqlServerStorage(seperateDB, new SqlServerStorageOptions
        {
          PrepareSchemaIfNecessary = true,
          // SchemaName = "Hangfire",
          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
          CommandTimeout = TimeSpan.FromSeconds(30),
          CountersAggregateInterval = TimeSpan.FromMinutes(5),
          DashboardJobListLimit = 50,
          // QueuePollInterval = TimeSpan.FromSeconds(15),
          // SqlClientFactory = SqlClientFactory.Instance,
          // UseRecommendedIsolationLevel = true,
          // JobExpirationCheckInterval = TimeSpan.FromHours(1),
          // EnableHeavyMigrations = false, // Only needed during major migration scenarios
          // Whether to disable global locks (default: false; set to true only in clustered scenarios with care)
          // DisableGlobalLocks = false,
          // SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5)
        });
      }
    });
    srvc.AddHangfireServer();
    srvc.Add_Hangfire_Srvcs(config);
  }
  public static void Use_Hangfire(this IApplicationBuilder app)
  {
    var option = app.GetSrvc<IOptions<Option_App>>().Value;

    var result = option.Hangfire.Users.Select(x => new HangfireCustomBasicAuthenticationFilter()
    {
      User = x.User,
      Pass = x.Pass
    }).ToList();

    app.UseHangfireDashboard($"/{option.ASPNETCORE_ROUTE_PREFIX}/hangfire"
      , new DashboardOptions()
      {
        DashboardTitle = $"Hangfire {option.Hangfire.Title}",
        AppPath = $"swagger/index.html", // Not Required -> Back To Site
        DarkModeEnabled = true,
        // PrefixPath = $"{option.ASPNETCORE_ROUTE_PREFIX}",
        DefaultRecordsPerPage = 50,
        Authorization = result
      }
     );

    // app.UseEndpoints(endpoints =>
    // {
    //   // http://localhost:1102/api/Job/v1/hangfire
    //   endpoints.MapHangfireDashboard(new DashboardOptions()
    //   {
    //     DashboardTitle = "Use EP Hangfire Title",
    //     AppPath = $"/hangfire",
    //   });
    // });

    app.Call_Hangfire_Recuring_Jobs();
  }

}