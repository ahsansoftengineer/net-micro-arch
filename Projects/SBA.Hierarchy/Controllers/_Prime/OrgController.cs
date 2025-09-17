using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers.Prime;
public class OrgController : Projectz_Default_Controller<OrgController, Org>
{
  public OrgController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.Orgs;
  }
}