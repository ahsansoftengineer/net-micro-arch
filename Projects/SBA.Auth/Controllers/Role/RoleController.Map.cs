
using GLOB.Domain.Model.Auth;
using Microsoft.AspNetCore.Mvc;

namespace SBA.Auth.Controllers;

public partial class RoleController : AccountBaseController<RoleController>
{
  [HttpGet("{role}")]
  public async Task<IActionResult> GetUserByRole(string role)
  {
    var roleExists = await _roleManager.RoleExistsAsync(role);
    if (!roleExists)
      return _Res.BadRequestModel("Role", $"Role '{role}' does not exist");

    var usersInRole = await _userManager.GetUsersInRoleAsync(role);

    var result = usersInRole.Select(u => new
    {
      u.Id,
      u.Email,
      u.UserName,
      u.Name,
      u.PhoneNumber,
      u.Status
    });

    return result.ToExtVMList().Ok();
  }
  
  [HttpGet("{userId}")]
  public async Task<IActionResult> GetRoleByUser(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user == null)
      return _Res.BadRequestzId("InfraUserId", userId);

    var roles = await _userManager.GetRolesAsync(user);

    return new
    {
      user.Id,
      user?.Email,
      Roles = roles
    }.ToExtVMSingle().Ok();
  }

  [HttpPost]
  public async Task<IActionResult> AddUserToRole([FromBody] AssignRoleToInfraUser dto)
  {
    var user = await _userManager.FindByEmailAsync(dto.Email);
    if (user == null)
      return _Res.BadRequestModel("Email", "Invalid Email not Exsist");

    var rolez = await _roleManager.FindByNameAsync(dto.Role);
    if (rolez == null)
      return _Res.BadRequestModel("Role", "Invalid Role not Exsist");

    // Check if user is already in role
    if (await _userManager.IsInRoleAsync(user, rolez.Name))
      return _Res.BadRequestModel("Role", $"{user?.Email} is already assigned to role {rolez.Name}");

    var result = await _userManager.AddToRoleAsync(user, rolez.Name);
    if (result.Succeeded)
    {
      return $"{user?.Email} has successfully been added to role {rolez.Name}".Ok();
    }

    return _Res.BadRequestModel("Exception", "Something went wrong");
  }
}