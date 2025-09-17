namespace GLOB.Domain.Hierarchy;
public class UserAgent : UserExtend
{
  public required string AgentCode { get; set; }
  public ICollection<UserCreator>? UserCreators { get; set; } = null;
}
