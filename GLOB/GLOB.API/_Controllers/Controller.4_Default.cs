namespace GLOB.API.Controllers.Base;
// Exting CRUD_SPO_Controller with Default Implementation
public abstract class API_4_Default_Controller<TController, TEntity> 
  : API_3_CRUD_SPO_Controller<TController, TEntity, DtoSearch, DtoCreate>
    where TController : class
    where TEntity : EntityBase, IEntityAlpha, IEntityStatus// (ID, Status, CreatedAt, UpdatedAt)  IEntityBeta, IEntityBase 
{
  public API_4_Default_Controller(IServiceProvider srvcProvider) : base(srvcProvider)
  {

  }
}