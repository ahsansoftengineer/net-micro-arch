using System.ComponentModel.DataAnnotations.Schema;
using GLOB.Domain.Mappings;
using Microsoft.AspNetCore.Http;

namespace GLOB.Domain.Hierarchy;
public class UserCreator : UserNormal
{
  // Bio = Description
  public string? UrlThumbnail { get; set; } = "";
  public string? UrlVideo { get; set; } = "";
  [NotMapped]
  public IFormFile? VideoFile { get; set; } = null;
  public bool IsSimpaisaUserCreated { get; set; } = false;
  public bool IsBusinessSuite { get; set; } = false;
  public int? StateID { get; set; } = null;

  // .-.
  public BankAccount? BankAccount { get; set; }
  public Pricing? Pricing { get; set; } = null;
  public SocialLink? SocialLink { get; set; } = null;

  // .-*
  public int? AgentID { get; set; } = null;
  public UserAgent? Agent { get; set; } = null;
  // public State? State { get; set; } = null;
  public int? CityID { get; set; } = null;
  public City? City { get; set; } = null;

  // *-.
  public ICollection<OrderBusiness>? OrderBusinesss { get; set; } = null;
  public ICollection<OrderStandard>? OrderStandards { get; set; } = null;
  // *-* Relationship
  [NotMapped]
  public ICollection<Mapping_UserCreatorIndustry>? UserCreatorIndustrys { get; set; } = null;
  [NotMapped]
  public ICollection<Mapping_UserCreatorProfession>? UserCreatorProfessions { get; set; } = null;

}
