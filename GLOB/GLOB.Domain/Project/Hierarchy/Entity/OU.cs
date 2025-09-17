using System.ComponentModel.DataAnnotations.Schema;

namespace GLOB.Domain.Hierarchy;
public class OU : EntityBase
{
  [ForeignKey(nameof(LE))]
  public int LEId { get; set; }
  public virtual LE? LE { get; set; }
  public virtual ICollection<SU>? SUs { get; set; }
  public string? Law { get; set; }
  public string? Address { get; set; }
  public string? Deposit { get; set; }
  public string? LogoImg { get; set; }
  public string? TopImg { get; set; }
  public string? WarningImg { get; set; }
  public string? FooterImg { get; set; }
}