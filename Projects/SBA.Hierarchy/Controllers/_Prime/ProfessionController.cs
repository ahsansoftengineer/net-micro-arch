using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers.Prime;
public class ProfessionController : Projectz_Default_Controller<ProfessionController, Profession>
{
  public ProfessionController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.Professions;
  }
}