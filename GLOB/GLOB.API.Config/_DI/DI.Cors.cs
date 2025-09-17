namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static void Add_API_Config_Cors_Gateway(this IServiceCollection srvc)
  {
    srvc.AddCors(opt =>
    {
      opt.AddDefaultPolicy(opt => 
        opt
          .AllowAnyHeader()
          .AllowAnyOrigin()
          .AllowAnyMethod()
      );
    });
  }
  public static void Add_API_Config_Cors_Auth(this IServiceCollection srvc)
  {
    srvc.AddCors(opt =>
    {
      opt.AddPolicy("PolicyAllowGateway",
        builder => builder
          .WithOrigins("http://localhost:1104", "https://localhost:1105")
          .AllowAnyHeader()
          .AllowAnyMethod());
    });
  }
  public static void Add_API_Config_Cors(this IServiceCollection srvc)
  {
    srvc.AddCors(opt =>
    {
      opt.AddPolicy("PolicyAllowGateway",
        builder => builder
          .WithOrigins("http://localhost:1100", "https://localhost:1101")
          .AllowAnyHeader()
          .AllowAnyMethod());
    });
  }
}