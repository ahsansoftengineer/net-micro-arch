using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GLOB.Domain.Model.Auth;
public class LoginDto
{
  [Required]
  [DataType(DataType.EmailAddress)]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [MinLength(7, ErrorMessage = "Your Password is limited to {1} characters")]
  // [MaxLength(30, ErrorMessage = "Your Password is limited to {1} characters")]
  [PasswordPropertyText]
  public string Password { get; set; }
}
public class UserDto : LoginDto
{
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  [DataType(DataType.PhoneNumber)]
  public string PhoneNumber { get; set; } = "";

  public ICollection<string>? Roles { get; set; }

}