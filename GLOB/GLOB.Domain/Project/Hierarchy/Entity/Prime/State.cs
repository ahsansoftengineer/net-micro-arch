
namespace GLOB.Domain.Hierarchy;
public class State : EntityBase
{
  public virtual ICollection<City>? Citys { get; set; }
}
