using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using GLOB.Domain.Model.Auth;

namespace GLOB.Infra.Data.Auth;

public static partial class InfraSeedIdentity
{
  public static IApplicationBuilder app;
  // Seed for Development through (CLI)
  public static void SeedInfraIdentity(this ModelBuilder mb)
  {
    "--> ModelBuilder -> InfraSeedIdentity -> SeedInfra".Print("EF Core");
    mb.SeedInfra();
    mb.SeedInfraRole();
    mb.SeedInfraUser();
    // mb.SeedRefreshToken();
  }
}

// Seed for Production (Automate)
// public static async Task SeedInfraIdentity(this IApplicationBuilder app)
// {
//   using(var srvcScp = app.ApplicationServices.CreateScope())
//   {

//     var srvc = srvcScp.ServiceProvider;
//     var contextz = srvc.GetSrvc<DBCtxInfra>();
//     var context = srvc.GetSrvc<DBCtxInfraIdentity>();
//     context.Database.EnsureCreated();
//     contextz.Database.EnsureCreated();
//     var userManager = srvc.GetRequiredService<UserManager<InfraUser>>();
//     var roleManager = srvc.GetRequiredService<RoleManager<InfraRole>>();

//     if (context != null)
//     {
//       "Infra Identity -> Applying Migrations AppBuilder (Prod)".Print("EF Core");
//       // context.Database.Migrate();
//       {
//         // app.SeedInfra();
//         await roleManager.SeedInfraRole();
//         await userManager.SeedInfraUser();
//         // await context.SeedRefreshToken();
//         // context.SeedRefreshToken();
//       }
//     }
//   }
// }