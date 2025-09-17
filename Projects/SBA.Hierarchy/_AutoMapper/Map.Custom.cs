using GLOB.API.Mapper;
using GLOB.Domain.Hierarchy;
using GLOB.Hierarchy.Global;

namespace SBA.Projectz.Mapper;
public partial class ProjectzMapper : API_Base_Mapper
{
  public ProjectzMapper() : base()
  {
  }
  public override void MapCustom() 
  {
    base.MapCustom();
    MapCRUD<GlobalLookup, GlobalLookupDtoCreate, GlobalLookupDtoRead, GlobalLookupDtoSearch>();

    MapCRUD<Systemz, SystemzDtoCreate, SystemzDtoRead, SystemzDtoSearch>();
    MapCRUD<LE, LEDtoCreate, LEDtoRead, LEDtoSearch>();
    MapCRUD<OU, OUDtoCreate, OUDtoRead, OUDtoSearch>();
    MapCRUD<SU, SUDtoCreate, SUDtoRead, SUDtoSearch>();
    MapCRUD<City, CityDtoCreate, CityDtoRead, CityDtoSearch>();
  }
}