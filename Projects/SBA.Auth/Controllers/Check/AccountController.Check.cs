using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace SBA.Auth.Controllers;

public partial class CheckAuthController :  AccountBaseController<CheckAuthController>
{
  public CheckAuthController(
    IServiceProvider srvcProvider
  ) : base(srvcProvider)
  {
  }

  // Jwt -> ✅ Login
  [HttpPost] [AllowAnonymous]
  public async Task<IActionResult> CheckAllowAnonymous()
  {
    return $"Check Allow Anonymous Successfull".Ok();
  }
  // Jwt -> ✅ Login
  [HttpPost] [Authorize]
  public async Task<IActionResult> CheckLogin()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    return $"Current User Id {id} Check Login".Ok();
  }

  // JWT -> ✅ Role OR
  [HttpPost] [Authorize(Roles = "Admin,Super Admin,Customer")]
  public async Task<IActionResult> CheckRoleOr()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    return $"Id: {id} Multi Roles OK! (Admin | Super Admin | Customer) ".Ok();
  }

  // JWT -> ✅ Role And
  [HttpPost] [Authorize(Roles = "Admin")] [Authorize(Roles = "Super Admin")] 
  public async Task<IActionResult> CheckRoleAnd()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    return $"Id: {id} Multi Roles OK! (Admin & Super Admin) ".Ok();
  }

  // JWT -> ✅ Policy-Admin
  [HttpPost] [Authorize(Policy = "Policy-Admin")]
  public async Task<IActionResult> CheckPolicyAdmin()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

    var roles = User.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value);

    var rolez = string.Join(", ", roles);

    return $"Id: {id}, IsAdmin: {User.IsInRole("Admin")}, Roles: (Policy-Admin)".Ok();
  }

  // JWT -> ✅ Policy-Customer
  [HttpPost] [Authorize(Policy = "Policy-Customer")]
  public async Task<IActionResult> CheckPolicyCustomer()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

    var roles = User.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value);

    var rolez = string.Join(", ", roles);

    return $"Id: {id}, IsCustomer: {User.IsInRole("Customer")}, Policy: (Policy-Customer)".Ok();
  }

  // JWT -> ✅ Policy Multi Or
  [HttpPost] [Authorize(Policy = "Policy-Admin--SuperAdmin")]
  public async Task<IActionResult> CheckPolicyMulti()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    return $"Id: {id} Multi Policy Checks OK! (Admin Or Super Admin)".Ok();
  }

  // Cookie -> ✅ Login
  [HttpPost] [Authorize(AuthenticationSchemes = "AuthorizationCookieScheme")]
  public async Task<IActionResult> CheckSchemeCookie()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    return $"Current User Id {id} Cookie Based Token Working".Ok();
  }
  // Cookie -> ✅ Role Admin
  [HttpPost] [Authorize(AuthenticationSchemes = "AuthorizationCookieScheme", Roles = "Admin")]
  public async Task<IActionResult> CheckSchemeCookieAdmin()
  {
    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    return $"Id: {id}, IsAdmin: {User.IsInRole("Admin")}, Scheme: Cookie, Role: Admin".Ok();
  }
}