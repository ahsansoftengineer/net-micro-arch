using Microsoft.AspNetCore.Authentication;

namespace SBA.Auth.Controllers;

public partial class AccountController
{
  [HttpGet]
  public IActionResult LoginMicrosoftRedirect([FromQuery] string? ReturnUrl)
  {
    return BaseLoginRedirect("Microsoft", "LoginMicrosoft", "Account", ReturnUrl);
  }

  [HttpGet]
  public async Task<IActionResult> LoginMicrosoft([FromQuery] string? ReturnUrl)
  {
    return await BaseLogin("Microsoft", ReturnUrl);
  }

  [HttpGet]
  public IActionResult LoginGoogleRedirect([FromQuery] string? ReturnUrl)
  {
    return BaseLoginRedirect("Google", "LoginGoogle", "Account", ReturnUrl);
  }
  [HttpGet]
  public async Task<IActionResult> LoginGoogle([FromQuery] string? ReturnUrl)
  {
    return await BaseLogin("Google", ReturnUrl);
  }

  [HttpGet]
  public IActionResult LoginFacebookRedirect([FromQuery] string? ReturnUrl)
  {
    return BaseLoginRedirect("Facebook", "LoginFacebook", "Account", ReturnUrl);
  }
  [HttpGet]
  public async Task<IActionResult> LoginFacebook([FromQuery] string? ReturnUrl)
  {
    return await BaseLogin("Facebook", ReturnUrl);
  }

  [HttpGet]
  public IActionResult LoginAppleRedirect([FromQuery] string? ReturnUrl)
  {
    return BaseLoginRedirect("Apple", "LoginApple", "Account", ReturnUrl);
  }
  [HttpGet]
  public async Task<IActionResult> LoginApple([FromQuery] string? ReturnUrl)
  {
    return await BaseLogin("Apple", ReturnUrl);
  }
  // Base Logins
  private IActionResult BaseLoginRedirect(string provider, string action, string controller, string? ReturnUrl)
  {
    var redirectUrl = Url.Action(action, controller, new { ReturnUrl });
    var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
    return Challenge(properties, provider);
  }
  private async Task<IActionResult> BaseLogin(string Provider, string? ReturnUrl)
  {
    var result = await HttpContext.AuthenticateAsync(Provider);
    if (!result.Succeeded) return Unauthorized();

    var claims = result.Principal.Claims.ToDictionary(c => c.Type, c => c.Value);
    return Ok(new { Provider, ReturnUrl, Claims = claims});
  }
}