using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers.Prime;
public class BrandController : Projectz_Default_Controller<BrandController, Brand>
{
  public BrandController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.Brands;
  }
}