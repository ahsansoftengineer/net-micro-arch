using System.Security.Claims;

using GLOB.Domain.Model.Auth;
using Microsoft.AspNetCore.Authorization;


namespace SBA.Auth.Controllers;

public partial class AccountController
{
  [HttpPost] [AllowAnonymous]
  public async Task<IActionResult> Login([FromBody] LoginDto model)
  {
    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    {
      return (await _tokenService.GetTokensAndUserClaims(user, HttpContext)).ToExtVMSingle().Ok();
    }
    return StatusCode(500, "An error occurred during login.");
  }

  [HttpPost] [AllowAnonymous]
  public async Task<IActionResult> LoginCookie([FromBody] LoginDto model)
  {
    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
    {
      var payload = await _tokenService.GetUserClaimSignInCookie(user, HttpContext);
      return payload.ToExtVMSingle().Ok();
    }

    return Unauthorized("Invalid email or password");
  }

  [HttpPost]
  public async Task<IActionResult> TokenRefresh([FromBody] RefreshTokenRequest request)
  {
    // Refresh Token is Used when the AccessToken expired and user not has to enter creadential again
    // Claim vs Principal
    // Claim = A claim is a key-value pair that represents information about the user.
    // Principal = A ClaimsPrincipal represents the current user.
    // ClaimsPrincipal ⟶ contains → ClaimsIdentity ⟶ contains → Claims
    var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
    if (principal == null)
      return _Res.BadRequestModel("AccessToken", "Invalid Access Token");

    var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
    var user = await _userManager.FindByIdAsync(userId);
    if (user == null)
      return _Res.BadRequestModel("AccessToken", "Invalid Access Token");

    var storedToken = await _ctx.RefreshTokens
        .Where(rt => rt.InfraUserId == userId
          && rt.Token == request.RefreshToken
          && !rt.IsRevoked && rt.ExpiresAt > DateTime.UtcNow)
        .FirstOrDefaultAsync();

    if (storedToken == null)
      return _Res.BadRequestModel("AccessToken", "Invalid / Expired Refresh Token");

    return (await _tokenService.GetTokensAndUserClaims(user, HttpContext)).ToExtVMSingle().Ok();
  }

  [HttpPost]
  public async Task<IActionResult> TokenRevoke([FromBody] RevokeTokenRequest request)
  {
    // The purpose of RevokeToken is to invalidate a refresh token so it can no longer be 
    // used to generate new access tokens — typically done on logout or when a token is suspected to be compromised.  
    if (string.IsNullOrWhiteSpace(request.RefreshToken))
      return BadRequest("Refresh token is required.");

    var storedToken = await _ctx.RefreshTokens
        .Include(rt => rt.InfraUser)
        .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

    if (storedToken == null)
      return _Res.BadRequestModel("RefreshToken", "Token does not exist.");

    if (storedToken.IsRevoked)
      return _Res.BadRequestModel("RefreshToken", "Token has already been revoked.");

    if (storedToken.ExpiresAt < DateTime.UtcNow)
      return _Res.BadRequestModel("RefreshToken", "Token has already expired.");

    // Optionally: only allow the currently authenticated user to revoke their token
    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (storedToken.InfraUserId != currentUserId)
      return _Res.BadRequestModel("RefreshToken", "You do not own this token.");
    // return Forbid("You do not own this token.");

    storedToken.IsRevoked = true;
    storedToken.RevokedAt = DateTime.UtcNow;
    storedToken.CreatedByIp = HttpContext.Connection.RemoteIpAddress?.ToString();

    await _ctx.SaveChangesAsync();

    return "Refresh token revoked successfully.".Ok();
  }

  [HttpGet]
  public async Task<IActionResult> Forbidden()
  {
    return StatusCode(403, "Invalid Request");
  }
}