using GLOB.Domain.Hierarchy;
using GLOB.Hierarchy.Global;

namespace SBA.Projectz.Mapper;
public partial class ProjectzMapper 
{
  public override void MapCommon() 
  {
    base.MapCommon();
    MapDefaults<GlobalLookupBase>();

    MapDefaults<Org>();
    MapDefaults<BG>();
    MapDefaults<Bank>();
    MapDefaults<Brand>();
    MapDefaults<Industry>();
    MapDefaults<Profession>();
    MapDefaults<State>();
  }
}