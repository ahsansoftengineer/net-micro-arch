using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers.Prime;
public class IndustryController : Projectz_Default_Controller<IndustryController, Industry>
{
  public IndustryController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.Industrys;
  }
}