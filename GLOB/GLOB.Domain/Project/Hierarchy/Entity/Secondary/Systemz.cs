using System.ComponentModel.DataAnnotations.Schema;

namespace GLOB.Domain.Hierarchy;
public class Systemz : EntityBase
{
  [ForeignKey(nameof(Org))]
  public int OrgId { get; set; } // We Marked it as Nullable because of Dynamic Filtering
  // [Relate] // For Eager Loading
  public virtual Org? Org { get; set; }
}

public class SystemzDtoRead : DtoRead
{
  public int OrgId { get; set; }
  public DtoSelect? Org { get; set; }
}
public class SystemzDtoCreate : DtoCreate
{
  public int OrgId { get; set; }
}
public class SystemzDtoSearch : DtoSearch
{
  public int? OrgId { get; set; }
}