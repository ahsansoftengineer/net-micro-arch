
namespace GLOB.Domain.Hierarchy;
public class City : EntityBase
{
  public int? StateId { get; set; }
  public virtual State? State { get; set; }
  // [NotMapped]
  // public ICollection<UserCreator>? UserCreators { get; set; }
}

public class CityDtoRead : DtoRead
{
  public int StateId { get; set; }
  public DtoSelect? State { get; set; }
}
public class CityDtoCreate : DtoCreate
{
  public int StateId { get; set; }
}
public class CityDtoSearch : DtoSearch
{
  public int? StateId { get; set; }
}