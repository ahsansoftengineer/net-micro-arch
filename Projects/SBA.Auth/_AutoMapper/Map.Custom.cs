using GLOB.API.Mapper;
using GLOB.Domain.Model.Auth;

namespace SBA.Projectz.Mapper;
public partial class ProjectzMapper : API_Base_Mapper
{
  public ProjectzMapper() : base()
  {
  }
  public override void MapCustom() 
  {
    base.MapCustom();
    MapCRUD<InfraUser, InfraUserDtoCreate, InfraUserDtoUpdate, InfraUserDtoRead, InfraUserDtoSearch, DtoSelect>();
    
    // MapCRUD<Systemz, SystemzDto, SystemzDtoCreate, SystemzDtoSearch>();
  }
}