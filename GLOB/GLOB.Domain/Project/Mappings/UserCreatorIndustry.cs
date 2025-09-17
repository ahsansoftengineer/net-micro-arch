using System.ComponentModel.DataAnnotations.Schema;
using GLOB.Domain.Hierarchy;

namespace GLOB.Domain.Mappings;
public class Mapping_UserCreatorIndustry
{
  // public int ID { get; set; }
  public int? UserCreatorID { get; set; }
  [NotMapped]
  public UserCreator? UserCreator { get; set; }

  public int? IndustryID { get; set; }
  [NotMapped]
  public Industry? Industry { get; set; }
}
