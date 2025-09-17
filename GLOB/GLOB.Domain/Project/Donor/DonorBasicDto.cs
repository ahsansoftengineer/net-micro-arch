
namespace GLOB.Domain.DTOs.Donor;
public class DonorBasicDto : DtoRead
{
  public string? Mobile { get; set; }
  public string? Email { get; set; }
  public string? Address { get; set; }
  public Gender? Gender { get; set; }// = Gender.None;

  public int DonorTypeId { get; set; }
  public DtoSelect? DonorType { get; set; }

  public int OrgId { get; set; }
  public DtoSelect? Org { get; set; }

  public int CityId { get; set; }
  public DtoSelect? City { get; set; }

}
public class DonorBasicDtoCreate : DtoCreate
{
  public string Mobile { get; set; } = string.Empty;
  public string? Email { get; set; }
  public string? Address { get; set; }
  public Gender? Gender { get; set; }// = Gender.None;

  public int DonorTypeId { get; set; }
  public int OrgId { get; set; }
  public int CityId { get; set; }
}
public class DonorBasicDtoSearch : DtoSearch
{
  public string? Mobile { get; set; }
  public string? Email { get; set; }
  public string? Address { get; set; }
  public Gender? Gender { get; set; }// = Gender.None;
  public int? DonorTypeId { get; set; }
  public int? OrgId { get; set; }
  public int? CityId { get; set; }
}