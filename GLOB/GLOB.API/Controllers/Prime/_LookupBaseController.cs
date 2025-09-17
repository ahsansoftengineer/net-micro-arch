


namespace SBA.Projectz.Controllers;

public class _ProjectzLookupBaseController : API_4_Default_Controller<_ProjectzLookupBaseController, ProjectzLookupBase>
{
  public _ProjectzLookupBaseController(
    IServiceProvider srvcProvider) : base(srvcProvider)
  {
    _repo = _uowInfra.ProjectzLookupBases;
  }
  
  [HttpPost]
  public async Task<IActionResult> CreateParentChild([FromBody] ProjectzLookupBaseDto data)
  {
    try
    {
      var result = _mapper.Map<ProjectzLookupBase>(data);
      await _repo.Add(result);
      // await _repo.GetDBSet().AddAsync(result);
      await _uowInfra.Save();
      return result.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      return ex.Message.Ok();
      // return _Res.CatchException(ex, nameof(CreateParentChild));
    }
  }
}