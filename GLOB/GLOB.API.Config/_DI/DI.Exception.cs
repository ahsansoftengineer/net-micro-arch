using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static void Use_API_Config_ExceptionHandler(this IApplicationBuilder app)
  {
    app.UseExceptionHandler(error =>
    {
      error.Run(async context =>
      {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
          //Log.Error($"Something Went Wrong in the {contextFeature.Error}"); //

          await context.Response.WriteAsync(new Error
          {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error. Please Try Again Later."
          }.ToString());
        }
      });
    });
  }

}
public class Error
{
  public int StatusCode { get; set; }
  public string Message { get; set; } = "";
  public override string ToString()
  {
    return JsonConvert.SerializeObject(this);
  }
}
// opt
// .AddPolicy("CorsPolicyAllowAll", builder =>
//   builder
//   .AllowAnyOrigin()
//   .AllowAnyMethod()
//   .AllowAnyHeader()
// .AllowCredentials()
// );

// opt.AddPolicy("AllowGateway", policy =>
// {
//     policy.WithOrigins("https://localhost:1101")
//           .AllowAnyHeader()
//           .AllowAnyMethod();
// });
// srvc.AddHttpsRedirection(opt =>
// {
//   opt.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
//   opt.HttpsPort = 1107; // Replace with your HTTPS port number if different
// });
//srvc.AddCors(opt =>
//{
//  opt.AddPolicy(MyAllowSpecificOrigins,
//            policy =>
//            {
//              policy.WithOrigins("http://example.com",
//                      "http://www.contoso.com")
//                      .AllowAnyHeader()
//                      .AllowAnyMethod();
//            });
//});