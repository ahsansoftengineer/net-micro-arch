using Microsoft.AspNetCore.Http;

namespace GLOB.Domain.Hierarchy;
public class OUDtoCreate : DtoCreate
{
  public int LEId { get; set; }
  public string? Law { get; set; }
  public string? Address { get; set; }
  public string? Deposit { get; set; }
  public IFormFile? LogoImg { get; set; }
  public IFormFile? TopImg { get; set; }
  public IFormFile? WarningImg { get; set; }
  public IFormFile? FooterImg { get; set; }
}
public class OUDtoCreateToEntity : DtoCreate
{
  public int LEId { get; set; }
  public string? Law { get; set; }
  public string? Address { get; set; }
  public string? Deposit { get; set; }
  public string? LogoImg { get; set; }
  public string? TopImg { get; set; }
  public string? WarningImg { get; set; }
  public string? FooterImg { get; set; }
}
public class OUDtoRead : DtoRead
{
  public int LEId { get; set; }
  public DtoSelect? LE { get; set; }
  public string? Law { get; set; }
  public string? Address { get; set; }
  public string? Deposit { get; set; }
  public string? LogoImg { get; set; }
  public string? TopImg { get; set; }
  public string? WarningImg { get; set; }
  public string? FooterImg { get; set; }

}
public class OUDtoSearch : DtoSearch
{
  public int? LEId { get; set; }
  public string? Law { get; set; }
  public string? Address { get; set; }
  public string? Deposit { get; set; }
}