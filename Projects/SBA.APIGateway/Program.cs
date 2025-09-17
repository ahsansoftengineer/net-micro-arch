using Serilog;

namespace SBA.APIGateway;
public class Program
{
  public static void Main(string[] args)
  {
    DI_API_Config.Reg_API_Config_Serilog();
    try
    {
      CreateHostBuilder(args).Build().Run();
    }
    catch (Exception e)
    {
      Log.Fatal(e, "Application Failed to start");
    }
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
      .UseSerilog()
      .ConfigureAppConfiguration((hostingContext, config) =>
      {
        var env = hostingContext.HostingEnvironment;
        var builtConfig = config.Build(); // Temporarily build config to read env vars
        var ocelotFile = builtConfig["OcelotFileName"] ?? "ocelot.json";

        config.AddJsonFile(ocelotFile, optional: false, reloadOnChange: true);
      })
      .ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      });
}
