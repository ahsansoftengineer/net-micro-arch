

namespace SBA.Auth.Controllers;

public partial class ProfileController : AccountBaseController<ProfileController>
{
  public ProfileController(
    IServiceProvider srvcProvider
  ) : base(srvcProvider)
  {
  }

  [HttpPost]
  public async Task<IActionResult> VerifyEmailToken([FromQuery] string email)
  {
    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
      return _Res.BadRequestzId("Email", email);

    if (user.EmailConfirmed)
      return _Res.BadRequestModel("Email", "Email already confirmed");

    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

    var encodedToken = Uri.EscapeDataString(token);

    // Send Email Here
    return new { token = encodedToken, message = "Email verification token generated" }.Ok();
  }
  [HttpPost]
  public async Task<IActionResult> VerifyEmail([FromQuery] string token, [FromQuery] string email)
  {
    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
      return _Res.BadRequestzId("Email", email);

    var result = await _userManager.ConfirmEmailAsync(user, token);
    if (!result.Succeeded)
      return _Res.BadRequestzId("Token", token);

    return "Your Email has been verified".Ok();
  }
  [HttpPost]
  public async Task<IActionResult> VerifyPhoneToken([FromQuery] string email)
  {
    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
      return _Res.BadRequestzId("Email", email);

    if (string.IsNullOrWhiteSpace(user.PhoneNumber))
      return _Res.BadRequestModel("PhoneNumber", "User does not have a phone number");

    var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

    // Send SMS Here
    return new { token, message = "Phone verification token generated" }.Ok();
  }
  [HttpPost]
  public async Task<IActionResult> VerifyPhone([FromQuery] string token, [FromQuery] string email)
  {
    if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(email))
      return _Res.BadRequestModel("Email & Token", "Email and token are required");

    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
      return _Res.BadRequestzId("Email", email);

    if (string.IsNullOrWhiteSpace(user.PhoneNumber))
      return _Res.BadRequestModel("PhoneNumber", "User does not have a phone number");

    var isTokenValid = await _userManager.VerifyChangePhoneNumberTokenAsync(user, token, user.PhoneNumber);
    if (!isTokenValid)
      return _Res.BadRequestModel("Token", "Invalid or expired phone verification token");

    user.PhoneNumberConfirmed = true;
    var result = await _userManager.UpdateAsync(user);

    if (!result.Succeeded)
      return result.Errors.BadRequestModel();

    return "Your phone number has been successfully verified".Ok();
  }
}