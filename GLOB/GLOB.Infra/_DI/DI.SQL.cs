
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Serilog;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GLOB.Infra.DI;

public static partial class DI_Infra
{

  public static void Add_Infra_DB_SQL<TContext, TIUOW, TUOW>(this IServiceCollection srvc, IConfiguration config)
    where TContext : DbContext
    where TIUOW : class, IUOW_Infra
    where TUOW : UOW_Infra, TIUOW
  {

    string connStr = config.GetConnectionString("SqlConnection");

    srvc.AddDbContext<TContext>(opt =>
    {
      opt.EnableDetailedErrors();
      opt.EnableSensitiveDataLogging();
      opt.UseSqlServer(connStr, sqlOptions =>
      {
        sqlOptions.EnableRetryOnFailure(
          maxRetryCount: 1,
          maxRetryDelay: TimeSpan.FromSeconds(3),
          errorNumbersToAdd: null
        );
      });
      // opt.LogTo(Log.Logger.Warning, LogLevel.Information);
      opt.LogTo(
        message =>
        {
          if (
              message.Contains("CommandExecuted", StringComparison.OrdinalIgnoreCase) &&
              !message.Contains("SELECT", StringComparison.OrdinalIgnoreCase) &&
              (message.Contains("INSERT", StringComparison.OrdinalIgnoreCase) ||
              message.Contains("UPDATE", StringComparison.OrdinalIgnoreCase) ||
              message.Contains("DELETE", StringComparison.OrdinalIgnoreCase))
          )
          {
            Log.ForContext("CUDSQL", true).Information(message); // 💡 Tagged specifically
          }
        },
        new[] { DbLoggerCategory.Database.Command.Name },
        LogLevel.Debug,
        DbContextLoggerOptions.Category | DbContextLoggerOptions.DefaultWithLocalTime
      );

    });

    srvc.AddScoped<TIUOW, TUOW>();
  }
}

// "ConnectionStrings": {
//   "SqlConnection": "Server=127.0.0.1,1430;Initial Catalog=SBA_Auth;User ID=sa;Password=P@55w0rd!123;TrustServerCertificate=true;"
// },

