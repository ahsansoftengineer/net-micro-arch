
using GLOB.Domain.Hierarchy;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers;
public class SystemzController : Project_RDS_Controller<SystemzController, Systemz>
{
  public SystemzController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.Systemzs;
  }
  [HttpPost]
  public async Task<IActionResult> GetsPaginate([FromBody] DtoRequestPage<SystemzDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginate(dto);
  }
  [HttpPost]
  public async Task<IActionResult> GetsPaginateOptions([FromBody] DtoRequestPage<SystemzDtoSearch?> dto)
  {
    return await _repo.ToActionGetsPaginateOptions(dto);
  }
  [HttpPost]
  public async Task<IActionResult> Add([FromBody] SystemzDtoCreate data)
  {
   
    bool hasParent = _uowProjectz.Orgs.AnyId(data.OrgId);
    if(!hasParent) return _Res.BadRequestzId("OrgId",data.OrgId);

    var result = _mapper.Map<Systemz>(data);
    await _repo.Add(result);
    await _uowInfra.Save();
    return Ok(result);
   
  }

  [HttpPut("{Id:int}")]
  public async Task<IActionResult> Update(int Id, [FromBody] SystemzDtoCreate data)
  {
    if (Id < 1) return _Res.NotFoundId(Id);
    
    var item = await _repo.Get(q => q.Id == Id);

    if (item == null) return _Res.NotFoundId(Id);

    bool hasParent = _uowProjectz.Orgs.AnyId(data.OrgId);
    if(!hasParent) return _Res.BadRequestzId("OrgId",data.OrgId);

    var result = _mapper.Map(data, item);
    _repo.Update(item);
    await _uowInfra.Save();
    return Ok(result);
   
  }
}