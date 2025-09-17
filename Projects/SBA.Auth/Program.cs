using Serilog;

namespace SBA.Auth;
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
    .ConfigureWebHostDefaults(webBuilder =>
    {
      webBuilder.UseStartup<Startup>();
    });
}