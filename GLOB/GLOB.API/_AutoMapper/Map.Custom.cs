
namespace GLOB.API.Mapper;
public partial class API_Base_Mapper 
{
  public virtual void MapCustom()
  {
    MapCRUD<ProjectzLookup, ProjectzLookupDtoCreate, ProjectzLookupDtoSearch>();
    CreateMap<ProjectzLookupBaseDto, ProjectzLookupBase>();
      // .Include<ProjectzLookupChild, ProjectzLookup>();
    CreateMap<ProjectzLookupChild, ProjectzLookup>(); 
  }
}