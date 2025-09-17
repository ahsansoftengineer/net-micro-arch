
namespace SBA.Projectz.Controllers;

public class _ProjectzLookupController : API_2_RDS_Controller<_ProjectzLookupController, ProjectzLookup>
{
  public _ProjectzLookupController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowInfra.ProjectzLookups;
  }

  [HttpPost]
  public async Task<IActionResult> GetsPaginate([FromBody] DtoRequestPage<ProjectzLookupDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginate(dto);
  }

  [HttpPost]
  public async Task<IActionResult> GetsPaginateOptions([FromBody] DtoRequestPage<ProjectzLookupDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginateOptions(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Add([FromBody] ProjectzLookupDtoCreate data)
  {
    try
    {
      bool hasParent = _uowInfra.ProjectzLookupBases.AnyId(data.ProjectzLookupBaseId);
      if (!hasParent) return _Res.BadRequestzId("ProjectzLookupBaseId", data.ProjectzLookupBaseId);

      var result = _mapper.Map<ProjectzLookup>(data);
      var entity = await _repo.Add(result);
      await _uowInfra.Save();
      return _Res.CreatedAtAction(this, nameof(Get), entity);
    }
    catch (Exception ex)
    {
      return _Res.CatchException(ex, nameof(Add));
    }
  }

  [HttpPut("{Id:int}")]
  public async Task<IActionResult> Update(int Id, [FromBody] ProjectzLookupDtoCreate data)
  {
    try
    {
      if (Id < 1) return _Res.NotFoundId(Id);

      var item = await _repo.Get(q => q.Id == Id);
      if (item == null) return _Res.NotFoundId(Id);

      bool hasParent = _uowInfra.ProjectzLookupBases.AnyId(data.ProjectzLookupBaseId);
      if (!hasParent) return _Res.BadRequestzId("ProjectzLookupBaseId", data.ProjectzLookupBaseId);

      var result = _mapper.Map(data, item);
      _repo.Update(item);
      await _uowInfra.Save();
      return result.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      return _Res.CatchException(ex, nameof(Update));
    }
  }
}