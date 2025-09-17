
namespace GLOB.Infra.Model.Base;

// Generic Interface
public interface IEntityAlpha<TKey>
{
  TKey Id { get; set; }
  string Name { get; set; }
}
public interface IEntityStatus<TKey>
{
  Status? Status { get; set; }
}
public interface IEntityBeta<TKey>
{
  DateTimeOffset? CreatedAt { get; set; }
  DateTimeOffset? UpdatedAt { get; set; }
}

public interface IEntityBase<TKey>
{
  string? Desc { get; set; }
  bool? IsSelected { get; set; }
}
