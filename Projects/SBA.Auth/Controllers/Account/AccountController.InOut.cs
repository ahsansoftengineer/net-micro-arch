
using GLOB.Domain.Model.Auth;
using Microsoft.AspNetCore.Authorization;

namespace SBA.Auth.Controllers;

public partial class AccountController : AccountBaseController<AccountController>
{
  // private IRepoGenericz<AccountId> _repo = null;
  public AccountController(
    IServiceProvider srvcProvider
  ) : base(srvcProvider)
  {
  }

  [HttpPost] [AllowAnonymous]
  public async Task<IActionResult> Register([FromBody] RegisterDto model) 
  {
    var user = UserController.MapUser(model);
    var result = await _userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
        return  await Login(new (){ Email = model.Email, Password = model.Password });
    }

    return result.Errors.BadRequestModel();
  }
  
  [HttpPost]
  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return Ok(new { message = "Logged out successfully" });
  }
}