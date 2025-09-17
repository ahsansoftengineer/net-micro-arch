using GLOB.Hierarchy.Global;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers.Prime;
public class _GlobalLookupBaseController  : Projectz_Default_Controller<_GlobalLookupBaseController , GlobalLookupBase>
{
  public _GlobalLookupBaseController (IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.GlobalLookupBases;
  }
}