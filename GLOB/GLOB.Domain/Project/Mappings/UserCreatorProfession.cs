using System.ComponentModel.DataAnnotations.Schema;
using GLOB.Domain.Hierarchy;

namespace GLOB.Domain.Mappings;
public class Mapping_UserCreatorProfession
{
  // public int ID { get; set; }
  public int? UserCreatorID { get; set; }
  [NotMapped]
  public UserCreator? UserCreator { get; set; }

  public int? ProfessionID { get; set; }
  [NotMapped]
  public Profession? Profession { get; set; }
}
