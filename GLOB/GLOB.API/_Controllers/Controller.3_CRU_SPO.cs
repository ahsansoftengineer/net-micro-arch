

namespace GLOB.API.Controllers.Base;
// Add, Read, Update, Delete, Status, Paginate, Options
public abstract partial class API_3_CRUD_SPO_Controller<TController, TEntity, TDtoSearch, TDtoCreate>
  : API_2_RDS_Controller<TController, TEntity>
  where TController : class
  where TEntity : class, IEntityAlpha, IEntityStatus
  where TDtoCreate : class
  where TDtoSearch : class, IDtoSearch
{
  public API_3_CRUD_SPO_Controller(IServiceProvider srvcProvider) : base(srvcProvider)
  {
  }
  [HttpPost] //[NoCache]
  public async Task<IActionResult> GetsPaginate([FromBody] DtoRequestPage<TDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginate(dto);
  }

  [HttpPost] //[NoCache]
  public async Task<IActionResult> GetsPaginateOptions([FromBody] DtoRequestPageOption<TDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginateOptions(dto);
  }

  [HttpPost] 
  public async Task<IActionResult> Add([FromBody] TDtoCreate data)
  {
    try
    {
      var mapped = _mapper.Map<TEntity>(data);
      var entity = await _repo.Add(mapped);
      await _uowInfra.Save();
      return _Res.CreatedAtAction(this, nameof(Get), entity);
    }
    catch (Exception ex)
    {
      return _Res.CatchException(ex, nameof(Add));
    }
  }

  [HttpPut("{Id:int}")]
  public async Task<IActionResult> Update(int Id, [FromBody] TDtoCreate data)
  {
    try
    {
      if (Id < 1) return _Res.NotFoundId(Id);
      
      var item = await _repo.Get(Id);

      if (item == null) return _Res.NotFoundId(Id);

      var result = _mapper.Map(data, item);
      var dataz = _repo.Update(result);
      await _uowInfra.Save();
      return dataz.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      return _Res.CatchException(ex, nameof(Update));
    }
  }

}