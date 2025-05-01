using Microsoft.Extensions.FileProviders;

namespace GLOB.API.Config.DI;
public static partial class API_DI_Common
{
  // FILES HANDING ###############
  public static void Config_StaticFilesHandling(this IApplicationBuilder app)
  {
    // https://localhost:5001/FooterImg348db3b7-e6dc-47f6-8bcd-74f6a35e7859.jpg
    // https://localhost:5001/assets/ouz/FooterImg348db3b7-e6dc-47f6-8bcd-74f6a35e7859.jpg
    // Optional: Serve files from a custom directory
    var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "assets");
    app.UseStaticFiles(new StaticFileOptions
    {
      FileProvider = new PhysicalFileProvider(staticFilesPath),
      RequestPath = "/assets"
    });
  }
  public static void Config_FileHandling(this IServiceCollection srvc)
  {
    srvc.Configure<IISServerOptions>(options =>
    {
      options.MaxRequestBodySize = int.MaxValue; // Set the maximum request body size (e.g., unlimited)
    });

    // srvc.AddHttpContextAccessor();// Already Config_d
    // srvc.AddScoped<FileUploderz, FileUploderz>();
  }
}