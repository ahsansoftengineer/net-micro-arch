using GLOB.Infra.Model.Base;

namespace GLOB.Domain.Hierarchy;
public class OrderBusinessMulti : EntityBeta
{
  public ICollection<OrderBusiness> OrderBusiness { get; set; } = [];
}
