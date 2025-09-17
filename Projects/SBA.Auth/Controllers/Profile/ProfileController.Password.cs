using System.Net;

using GLOB.Domain.Model.Auth;
using Microsoft.AspNetCore.Authorization;

namespace SBA.Auth.Controllers;

public partial class ProfileController
{
  [HttpPost] [AllowAnonymous]
  public async Task<IActionResult> PasswordForgot([FromQuery] string email)
  {
    if (!ModelState.IsValid)
      return _Res.BadRequestModel(ModelState);

    var user = await _userManager.FindByEmailAsync(email);
    if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
      return "If the email exists, a reset link will be sent.".Ok();

    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    var encodedToken = WebUtility.UrlEncode(token);

    string url = _config.GetWebUrl();
    var resetLink = $"{url}/profile/password-reset?email={email}&token={encodedToken}";
    // Send Email Here
    // await _emailSender.SendEmailAsync(user?.Email, "Reset Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");

    return new { message = "Reset link sent to email.", token = resetLink }.Ok();
  }

  [HttpPost] [AllowAnonymous]
  public async Task<IActionResult> PasswordReset([FromBody] ResetPasswordDto model)
  {
    if (!ModelState.IsValid)
      return _Res.BadRequestModel(ModelState);

    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user == null)
      return _Res.NotFoundId("Email", model.Email);

    var result = await _userManager.ResetPasswordAsync(user, WebUtility.UrlDecode(model.Token), model.NewPassword);

    if (result.Succeeded)
      return "Password has been reset.".Ok();

    return result.Errors.BadRequestModel();
  }

  [HttpPost]
  public async Task<IActionResult> PasswordChange([FromBody] ChangePasswordDto model)
  {
    if (model.NewPassword != model.ConfirmPassword)
        return _Res.BadRequestzId("newPassword, confirmPassword", "passwords does not match.");

    var user = await _userManager.GetUserAsync(User);
    if (user == null)
        return Unauthorized();

    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    if (!result.Succeeded)
      return  result.Errors.BadRequestModel();

    return "Password changed successfully.".Ok();
  }

}