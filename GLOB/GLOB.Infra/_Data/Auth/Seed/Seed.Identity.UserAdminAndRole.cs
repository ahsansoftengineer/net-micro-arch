using System.Security.Claims;
using GLOB.Domain.Model.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GLOB.Infra.Data.Auth;
public static partial class InfraSeedIdentity
{
  public static async Task SeedInfraRolesAndAdmin(IServiceProvider srvc)
  {
    await srvc.SeedRoles();
    await srvc.SeedRolesAndUser("admin@yopmail.com", ROLE.ADMIN);
    await srvc.SeedRolesAndUser("user_admin@yopmail.com", ROLE.ADMIN);
    await srvc.SeedRolesAndUser("user_manager@yopmail.com", ROLE.MANAGER);
    await srvc.SeedRolesAndUser("user_creator@yopmail.com", ROLE.USER_CREATOR);
    await srvc.SeedRolesAndUser("user_buiness@yopmail.com", ROLE.USER_BUSINESS);
    await srvc.SeedRolesAndUser("user_standard@yopmail.com", ROLE.USER_STANDARD);
    await srvc.SeedRolesAndUser("user_agent@yopmail.com", ROLE.USER_AGENT);
  }
  private static async Task SeedRoles(this IServiceProvider srvc)
  {
    var roleManager = srvc.GetRequiredService<RoleManager<InfraRole>>();
    foreach (var role in ROLE.ROLES)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new InfraRole(role));
      }
    }
  }
  private static async Task SeedRolesAndUser(this IServiceProvider srvc, string email, string ROLE)
  {
    email = email.ToLower();
    var userManager = srvc.GetRequiredService<UserManager<InfraUser>>();

    var data = await userManager.FindByEmailAsync(email);
    if (data == null)
    {
      string normalized = email.ToUpper();
      data = new InfraUser
      {

        Id = Guid.NewGuid().ToString(),
        UserName = email,
        Email = email,
        NormalizedUserName = normalized,
        NormalizedEmail = normalized,
        EmailConfirmed = true,
      };
      var result = await userManager.CreateAsync(data, "Admin@123");
      if (result.Succeeded)
      {
        await userManager.AddToRoleAsync(data, ROLE);
        IEnumerable<Claim> claims = [
          new Claim("ManageUsers", "true")
        ]; 
        await userManager.AddClaimsAsync(data, claims);
      }
    }
  }
}