namespace GLOB.API.Config.DI;
public static partial class DI_API_Config
{
  public static void Add_API_Config_Caching(this IServiceCollection srvc)
  {
    // API Throttling 1: Adding Service
    // srvc.AddMemoryCache(); // Enable Production
    // API Throttling 3
    // srvc.Add_API_Config_RateLimiting();
    srvc.AddHttpContextAccessor();
    // API Caching 6: Adding Services Extensions
    srvc.Add_API_Config_HttpCacheHeaders();
  }
  // API Caching : 5 with Marvin.Cache.Headers
  public static void Add_API_Config_HttpCacheHeaders(this IServiceCollection srvc)
  {
    srvc.AddResponseCaching();
    srvc.AddHttpCacheHeaders(
      (expirationOpt) =>
      {
        expirationOpt.MaxAge = 5;
        expirationOpt.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
      },
      (validationOpt) =>
      {
        validationOpt.MustRevalidate = true;
      }
      );
  }
  // CACHING VERSIONING ######################
  public static void Use_API_Config_Caching(this IApplicationBuilder app)
  {
    // API Caching 2. Setting up Caching
    // app.UseResponseCaching(); // Enable Production
    // API Caching 7. Setting up Caching Profile at Globally
    // app.UseHttpCacheHeaders(); 
    // Enable Production
    // API Throttling 4. Setting up Middleware
    // This is giving error while running
    // app.UseIpRateLimiting(); // Prevent Unnecessary Http Call from same User
  }
}