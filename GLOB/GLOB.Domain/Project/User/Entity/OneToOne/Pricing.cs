using System.ComponentModel.DataAnnotations.Schema;

namespace GLOB.Domain.Hierarchy;
public class Pricing : EntityBeta
{
  public int? UserCreatorID { get; set; }
  public UserCreator? UserCreator { get; set; }
  [NotMapped]
  public bool IsSoldOut
  {
    get
    {
      return !StandardIsActive && !CustomIsActive && !BusinessIsActive;
    }
  }

  // Personal Shoutout
  public float PersonalOrderCommission { get; set; } = 0;
  public float DiscountPercentage { get; set; } = 0;
  public int ResponseTime { get; set; } = 7;
  public DateTimeOffset? _DiscountExpiry { get; set; }
  [NotMapped]
  public DateTimeOffset? DiscountExpiry
  {
    get
    {
      return _DiscountExpiry;
    }
    set
    {
      if (value.HasValue)
      {
        _DiscountExpiry = value.Value.Date.AddDays(1).AddTicks(-1);
      }
      else
      {
        _DiscountExpiry = null;
      }
    }
  }
  [NotMapped]
  public bool IsDiscount
  {
    get
    {
      if (!DiscountExpiry.HasValue)
        return false;
      return DiscountExpiry.Value > DateTimeOffset.Now;
    }
  }

  //// Standard
  public bool StandardIsActive { get; set; } = false;
  public int Standard { get; set; } = 0;

  //// Custom
  public bool CustomIsActive { get; set; } = false;
  public int Custom { get; set; } = 0;
  // Business Shoutout
  public bool BusinessIsActive { get; set; } = false;
  public int Business_VideoOnly { get; set; } = 0;
  public int Business_VideoRepostToStory { get; set; } = 0;
  public int Business_VideoStory { get; set; } = 0;
  public int Business_VideoPost { get; set; } = 0;
  public float BusinessOrderCommission { get; set; } = 0;
}
