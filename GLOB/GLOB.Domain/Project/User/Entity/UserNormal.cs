using System.ComponentModel.DataAnnotations.Schema;
namespace GLOB.Domain.Hierarchy;
public abstract class UserNormal : BaseUser
{
  public bool AgreeToTermsCondition { get; set; } = false;
  public bool AgreeToPrivacyPolicy { get; set; } = false;
  [Column("Login_At")]
  public DateTimeOffset? LoginAt { get; set; } = null;

}
public abstract class UserExtend : UserNormal
{
  public string? UserType { get; set; }
}
