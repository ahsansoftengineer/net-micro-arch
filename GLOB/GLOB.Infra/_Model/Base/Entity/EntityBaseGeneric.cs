using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace GLOB.Infra.Model.Base;

public abstract class EntityAlpha<TKey> : IEntityAlpha<TKey>, IEntityStatus
  where TKey : IEquatable<TKey>
{
  [Column(Order = 1)]
  [Key]
  public TKey Id { get; set; }

  [Column(Order = 2)]
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public Status? Status { get; set; } = Constantz.Status;

  [Column(Order = 3)] // required
  [Unicode(false)]
  [MaxLength(100)]
  public string Name { get; set; }
}
public abstract class EntityBeta<TKey> : EntityAlpha<TKey>, IEntityBeta
  where TKey : IEquatable<TKey>
{
  [Column("Created_At")]
  public DateTimeOffset? CreatedAt { get; set; } = Constantz.Date;

  [Column("Updated_At")]
  public DateTimeOffset? UpdatedAt { get; set; } = Constantz.Date;
}

public abstract class EntityBase<TKey> : EntityBeta<TKey>, IEntityBase
  where TKey : IEquatable<TKey>
{

  [Column(Order = 4)]
  [Unicode(false)]
  [MaxLength(250)]
  public string? Desc { get; set; }

  [NotMapped]
  public bool? IsSelected { get; set; } = false;
}