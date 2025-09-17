using System.ComponentModel;

namespace GLOB.Infra.Model.Base;

public class DtoRequestGet
{
  [DefaultValue(null)]
  public List<string>? Includes { get; set; }
}

public class DtoRequestStatus<TKey> : DtoRequestDelete<TKey>
{
  public Status Status { get; set; } = Status.None;
}
public class DtoRequestStatus : DtoRequestDelete
{
  public Status Status { get; set; } = Status.None;
}
public class DtoRequestDelete<TKey>
{
  public TKey? Id { get; set; }
}
public class DtoRequestDelete
{
  public int? Id { get; set; }
}

public class DtoRequestGetByIds : DtoRequestGetByIds<int> { }
public class DtoRequestGetByIds<TKey>
{
  public List<TKey>? Ids { get; set; } = null;
}