
using GLOB.Hierarchy.Global;
using SBA.Projectz.Controllers.Base;

namespace SBA.Hierarchy.Controllers;
public class _GlobalLookupController : Project_RDS_Controller<_GlobalLookupController, GlobalLookup>
{
  public _GlobalLookupController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowProjectz.GlobalLookups;
  }

  [HttpPost]
  public async Task<IActionResult> GetsPaginate([FromBody] DtoRequestPage<GlobalLookupDtoSearch?> req)
  {
    return await _repo.ToActionGetsPaginate(req);
  }
  [HttpPost]
  public async Task<IActionResult> GetsPaginateOptions([FromBody] DtoRequestPage<GlobalLookupDtoSearch?> req)
  {
    return await _repo.ToActionGetsPaginateOptions(req);
  }
  [HttpPost]
  public async Task<IActionResult> Add([FromBody] GlobalLookupDtoCreate data)
  {
    try
    {
      bool hasParent = _uowProjectz.GlobalLookupBases.AnyId(data.GlobalLookupBaseId);
      if(!hasParent) return _Res.BadRequestzId("GlobalLookupBaseId",data.GlobalLookupBaseId);
      
      var result = _mapper.Map<GlobalLookup>(data);
      var entity = await _repo.Add(result);
      await _uowProjectz.Save();
      return _Res.CreatedAtAction(this, nameof(Get), entity);
    }
    catch (Exception ex)
    {
      return _Res.CatchException(ex, nameof(Add));
    }
  }

  [HttpPut("{Id:int}")]
  public async Task<IActionResult> Update(int Id, [FromBody] GlobalLookupDtoCreate data)
  {
    var item = await _repo.Get(q => q.Id == Id);
    
    bool hasParent = _uowProjectz.GlobalLookupBases.AnyId(data.GlobalLookupBaseId);
    if(!hasParent) return _Res.BadRequestzId("GlobalLookupBaseId",data.GlobalLookupBaseId);

    var result = _mapper.Map(data, item);
    _repo.Update(item);
    await _uowProjectz.Save();
    return result.ToExtVMSingle().Ok();
  }
}