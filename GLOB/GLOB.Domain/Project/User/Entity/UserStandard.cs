namespace GLOB.Domain.Hierarchy;
public class UserStandard : UserExtend
{
  public ICollection<OrderStandard>? OrderStandards { get; set; } = null;
}
