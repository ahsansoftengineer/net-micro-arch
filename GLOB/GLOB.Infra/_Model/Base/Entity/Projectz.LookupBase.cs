namespace GLOB.Infra.Model.Base;

public class ProjectzLookupBase : EntityBase
{
  public virtual ICollection<ProjectzLookup>? ProjectzLookups { get; set; }
}

public class ProjectzLookupBaseDto : DtoCreate
{
  public ICollection<ProjectzLookupChild>? ProjectzLookups { get; set; }
}

public class ProjectzLookupChild : DtoCreate
{
  public int ProjectzLookupBaseId { get; set; }
}