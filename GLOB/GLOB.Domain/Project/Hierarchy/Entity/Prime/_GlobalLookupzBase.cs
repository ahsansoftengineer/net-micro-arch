using GLOB.Infra.Model.Base;

namespace GLOB.Hierarchy.Global;

public class GlobalLookupBase : EntityBase
{
  public virtual ICollection<GlobalLookup>? GlobalLookup { get; set; }
}