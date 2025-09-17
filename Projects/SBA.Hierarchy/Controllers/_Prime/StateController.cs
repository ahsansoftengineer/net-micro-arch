using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers.Prime;
public class StateController : Projectz_Default_Controller<StateController, State>
{
  public StateController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.States;
  }
}