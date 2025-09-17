using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers.Prime;
public class BankController : Projectz_Default_Controller<BankController, Bank>
{
  public BankController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.Banks;
  }
}