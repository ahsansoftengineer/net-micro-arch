using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;

using GLOB.API.Config.OptionSetup;
using Microsoft.Extensions.Options;

namespace GLOB.API.Config.DI;

public static partial class DI_API_Config
{
public static void Add_API_Config_Controller(
    this IServiceCollection srvc,
    IConfiguration config,
    Action<MvcOptions>? configureMvcOptions = null)
  {
    // var appConfig = config.Get<Option_App>();
    // appConfig.Print("ENV");

    srvc
      // API Caching 3. Defining Cache Profile
      .AddControllers(opt =>
      {
        opt.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseRouteTransformer()));
        opt.Conventions.Insert(0, new GlobalRoutePrefixConvention(config.GetValueStr("ASPNETCORE_ROUTE_PREFIX")));

        opt.CacheProfiles.Add("120SecondsDuration", new CacheProfile
        {
          Duration = 5
          //,Location = ResponseCacheLocation.Client
          //,NoStore = true
          //,VaryByHeader = "I don't know which string"
          //,VaryByQueryKeys = "Any Keys"
        });
        configureMvcOptions?.Invoke(opt);

        // NOTE: TODO: Uncomment For Auth
        // var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        // opt.Filters.Add(new AuthorizeFilter(policy));
      })
      .ConfigureApiBehaviorOptions(opt =>
      {
        opt.InvalidModelStateResponseFactory = context =>
        {
          var errors = context.ModelState.ToExtValidationError();
          return new BadRequestObjectResult(new
          {
            Errors = errors,
            Message = "Bad Request, Validation Failed"
          });
        };
      })
      // Config for Model Prop to KebabCase
      // .AddJsonOptions(opt =>
      // {
      //   opt.JsonSerializerOptions.PropertyNamingPolicy = new KebabCaseNamingPolicy();
      // })
      .AddNewtonsoftJson(opt =>
      {
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        opt.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
      });

  }

  public static void Use_API_Config_Controller(this IApplicationBuilder app)
  {
    Option_App appConfig = app.GetSrvc<IOptions<Option_App>>().Value;
    appConfig.Print("ENV");

    app.UseEndpoints(ep =>
    {
      ep.MapControllers();
    });
    app.Use(async (context, next) =>
    {
      string path = context.Request.Path;
      if (path == "/")
      {
        //"api/Hierarchy/v1";
        context.Response.Redirect($"/{appConfig.ASPNETCORE_ROUTE_PREFIX}/swagger/index.html");
        return;
      }
      await next();
    });
  }
  
}