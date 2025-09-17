using System.ComponentModel.DataAnnotations.Schema;

namespace GLOB.Domain.Hierarchy;
public class LE : EntityBase
{
  [ForeignKey(nameof(BG))]
  public int BGId { get; set; }
  public virtual BG? BG { get; set; }
  public virtual ICollection<OU> OUs { get; set; }

}

public class LEDtoCreate : DtoCreate
{
  public int BGId { get; set; }
}
public class LEDtoRead : DtoRead
{
  public int BGId { get; set; }
  public DtoSelect? BG { get; set; }
}
public class LEDtoSearch : DtoSearch
{
  public int? BGId { get; set; }
}