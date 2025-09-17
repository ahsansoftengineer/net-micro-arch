using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static void Add_API_Config_Authorization_JWT(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.AddAuthorization(options =>
    {
      options.AddPolicy("Policy-Admin", policy => policy.RequireRole("Admin"));
      options.AddPolicy("Policy-Customer", policy => policy.RequireRole("Customer"));

      options.AddPolicy("Policy-Admin--SuperAdmin", policy => policy.RequireRole("Admin", "Super Admin"));

      // Enable Both (JWT and Cookie) 
      options.AddPolicy("JwtOrCookie", policy =>
      {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "Bearer", Option_JwtSettings.Scheme);
        policy.RequireAuthenticatedUser();
      });
    });
  }
}