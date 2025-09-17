
using GLOB.Domain.Model.Auth;

namespace SBA.Auth.Controllers;

public partial class UserController : AccountBaseController<UserController>
{
  private IQueryable<InfraUser> _repo;
  public UserController(
    IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _userManager.Users;
  }
 
  [HttpPost]
  public async Task<IActionResult> Add([FromBody] RegisterDto model)
  {
    InfraUser user = MapUser(model);
    var result = await _userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
      return Ok(new { message = "User registered successfully!" });
    }

    return result.Errors.BadRequestModel();
  }
  [HttpPut("{Id}")]
  public async Task<IActionResult> Update(string Id, [FromBody] UpdateUserDto data)
  {
    if (string.IsNullOrEmpty(Id)) return _Res.NotFoundId(Id);

    var item = await _userManager.FindByIdAsync(Id);

    if (item == null) return _Res.NotFoundId(Id);
    item.Name = data.FullName;
    item.PhoneNumber = data.PhoneNumber;

    var result = await _userManager.UpdateAsync(item);
    await _uowInfra.Save();
    return Ok(result);

  }

  [HttpDelete("{Id}")]
  public async Task<IActionResult> Delete(string Id)
  {
    if (string.IsNullOrEmpty(Id)) return _Res.NotFoundId(Id);

    var item = await _userManager.FindByIdAsync(Id);
    // if (item == null) return _Res.NotFoundId(Id);

    await _userManager.DeleteAsync(item);
    return NoContent();
  }
  [HttpPatch("{Id}")]
  public async Task<IActionResult> UpdateStatus(string? Id, [FromBody] DtoRequestStatus dto)
  {

    if (string.IsNullOrEmpty(Id)) return _Res.NotFoundId(Id);
    if (!Enum.IsDefined(dto.Status)) return _Res.InvalidEnums(dto.Status.ToString());

    var item = await _userManager.FindByIdAsync(Id);

    if (item == null) return _Res.NotFoundId(Id);

    item.Status = dto.Status;
    await _userManager.UpdateAsync(item);
    await _uowProjectz.Save();

    var result = _mapper.Map<InfraUserDtoRead>(item);
    return Ok(result);

  }
  internal static InfraUser MapUser(RegisterDto model)
  {
    return new InfraUser
    {
      Id = Guid.NewGuid().ToString(),
      UserName = model.Email,
      Email = model.Email,
      PhoneNumber = model.PhoneNumber,
      Name = model.FullName
    };
  }
}