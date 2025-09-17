using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static AuthenticationBuilder Add_API_Config_Authentication_JWT(this IServiceCollection srvc, IConfiguration config)
  {
    IHostEnvironment env = srvc.BuildServiceProvider().GetRequiredService<IHostEnvironment>();

    var jwt = new Option_JwtSettings();
    config.GetSection(Option_JwtSettings.SectionName).Bind(jwt);

    return srvc.AddAuthentication(options =>
    {
      options.Config_AuthenticationOptions();
    })
    .AddJwtBearer(options =>
    {
      options.SaveToken = true;
      options.RequireHttpsMetadata = env.IsProduction();
      options.TokenValidationParameters = new TokenValidationParameters()
      {
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        ValidateIssuer = jwt.ValidateIssuer,
        ValidateLifetime = jwt.ValidateLifetime,
        ValidateAudience = jwt.ValidateAudience,
        ValidateIssuerSigningKey = jwt.ValidateIssuerSigningKey,
        IssuerSigningKey = jwt.GetSymmetricSecurityKey(),
        ClockSkew = TimeSpan.Zero, // Optional: Adjust clock skew as needed

        // JWT Roles Config
        // RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        // NameClaimType = ClaimTypes.NameIdentifier
      };
      options.Events = new JwtBearerEvents
      {
        OnChallenge = context =>
        {
          context.HandleResponse(); // Suppress default 302 redirect
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          context.Response.ContentType = "application/json";
          return context.Response.WriteAsync("{\"error\": \"Unauthorized\"}");
        },
        OnTokenValidated = async context =>
        {
          // var jti = context.Principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
          // var db = context.HttpContext.RequestServices.GetRequiredService<DBCtxProjectz>();
          // var isRevoked = await db.RevokedTokens.AnyAsync(t => t.Jti == jti);

          // if (isRevoked)
          // {
          //   context.Fail("Access token has been revoked.");
          // }
        }
      };
    })
    .AddCookie(Option_JwtSettings.Scheme, options =>
    {
      options.LoginPath = jwt.LoginPath; // Specify your login path if required
      options.Cookie.Name = jwt.CookieName;
      options.AccessDeniedPath = jwt.AccessDeniedPath;
      options.ExpireTimeSpan = TimeSpan.FromMinutes(jwt.AccessTokenExpiryMinutes);
      options.SlidingExpiration = true; // cookie’s lifetime will renew on every request
    });
   
  }

  public static void Add_API_Config_JWT_Option(this IServiceCollection srvc)
  {
    srvc.PostConfigure<AuthenticationOptions>(options =>
    {
      options.Config_AuthenticationOptions();
    });
  }

  private static void Config_AuthenticationOptions(this AuthenticationOptions options)
  {
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  }
}