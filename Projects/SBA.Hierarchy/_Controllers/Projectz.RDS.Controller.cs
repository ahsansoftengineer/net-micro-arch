
namespace SBA.Projectz.Controllers.Base;
public abstract partial class Project_RDS_Controller<TController, TEntity>
    : API_2_RDS_Controller<TController, TEntity>
  where TController : class
  where TEntity : EntityBase
{
  protected readonly IUOW_Projectz _uowProjectz;
  public Project_RDS_Controller(IServiceProvider srvcProvider) : 
    base(srvcProvider)
  {
    _uowProjectz = _sp.GetSrvc<IUOW_Projectz>();
  }
}