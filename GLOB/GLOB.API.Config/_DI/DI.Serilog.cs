using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;

namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static void Reg_API_Config_Serilog()
  {
    // Verbose,Debug,Information,Warning,Error,Fatal
    Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Information() // or Information in production
      .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
      .MinimumLevel.Override("System", LogEventLevel.Warning)
      // .MinimumLevel.Override("Hangfire.Storage.SQLite.ExpirationManager", LogEventLevel.Warning)
      // .MinimumLevel.Override("Hangfire.Server.BackgroundServerProcess", LogEventLevel.Warning)
      .Enrich.FromLogContext()
      .WriteTo.Console(
          outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
          theme: AnsiConsoleTheme.Sixteen
      )
       .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(logEvent =>
            logEvent.Properties.ContainsKey("CUDSQL") &&
            logEvent.Properties["CUDSQL"].ToString() == "True")
        .WriteTo.File(
            "Logs/log.txt",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Debug,
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    )
    .CreateLogger();
  }
}