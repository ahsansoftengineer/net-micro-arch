
using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers;
public class CityController : Project_RDS_Controller<CityController, City>
{
  public CityController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.Citys;
  }
  [HttpPost]
  public async Task<IActionResult> GetsPaginate([FromBody] DtoRequestPage<CityDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginate(dto);
  }
  [HttpPost]
  public async Task<IActionResult> GetsPaginateOptions([FromBody] DtoRequestPage<CityDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginateOptions(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Add([FromBody] CityDtoCreate data)
  {
   
    bool hasParent = _uowProjectz.States.AnyId(data.StateId);
    if(!hasParent) return _Res.BadRequestzId("StateId",data.StateId);

    var result = _mapper.Map<City>(data);
    var entity = await _repo.Add(result);
    await _uowInfra.Save();
    return _Res.CreatedAtAction(this, nameof(Get), entity);
   
  }

  [HttpPut("{Id:int}")]
  public async Task<IActionResult> Update(int Id, [FromBody] CityDtoCreate data)
  {
    
    if (Id < 1) return _Res.NotFoundId(Id);

    var item = await _repo.Get(q => q.Id == Id);
    if (item == null) return _Res.NotFoundId(Id);
    
    bool hasParent = _uowProjectz.States.AnyId(data.StateId);
    if(!hasParent) return _Res.BadRequestzId("StateId",data.StateId);

    var result = _mapper.Map(data, item);
    _repo.Update(item);
    await _uowInfra.Save();
    return NoContent();
   
  }
}