
namespace GLOB.Domain.Hierarchy;
public class SocialLink : EntityBeta
{
  public int? UserCreatorID { get; set; }
  public UserCreator? UserCreator { get; set; }
  public string? Instagram { get; set; }
  public string? Facebook { get; set; }
  public string? Youtube { get; set; }
  public string? Twitter { get; set; }
  public string? Tiktok { get; set; }
  public decimal? TotalFollowers { get; set; }
  public decimal? TotalEngagementRate { get; set; }
  // IsActive
  // Name = Instagram, Facebook, Youtube, Twitter, Tiktok
  // Desc = www.Instagram.com?>>>??>?>?>
}
