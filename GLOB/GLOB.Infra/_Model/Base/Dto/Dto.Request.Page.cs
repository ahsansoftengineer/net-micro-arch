namespace GLOB.Infra.Model.Base;
public enum Order
{
  Unspecified = -1,
  Ascending,
  Descending
}
public class Sort
{
  public string? By { get; set; }
  public Order? Order { get; set; } = Base.Order.Unspecified;
}
public class DtoRequestPageOption<TDtoSearch>
{
  public int PageNo { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public TDtoSearch? Filter { get; set; }
  public Sort? Sort { get; set; }
}
public class DtoRequestPageOption : DtoRequestPageOption<DtoSearch>
{
}
public class DtoRequestPageNoInclude : DtoRequestPageOption<DtoSearch?>
{
}
public class DtoRequestPageNoInclude<TDtoSearch> : DtoRequestPageOption<TDtoSearch>
{
}

public class DtoRequestPage<TDtoSearch> : DtoRequestPageOption<TDtoSearch>
{
  public List<string?>? Includes { get; set; }
}

public class DtoPage()
{
  public int PageSize { get; set; } = 10;
  public int PageNo { get; set; } = 1;
}