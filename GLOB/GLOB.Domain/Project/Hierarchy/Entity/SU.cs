using System.ComponentModel.DataAnnotations.Schema;

namespace GLOB.Domain.Hierarchy;
public class SU : EntityBase
{
  [ForeignKey(nameof(OU))]
  public int OUId { get; set; }
  public virtual OU? OU { get; set; }

}

public class SUDtoCreate : DtoCreate
{
  public int OUId { get; set; }
}

public class SUDtoRead : DtoRead
{
  public int OUId { get; set; }
  public DtoSelect? OU { get; set; }
}
public class SUDtoSearch : DtoSearch
{
  public int? OUId { get; set; }
}