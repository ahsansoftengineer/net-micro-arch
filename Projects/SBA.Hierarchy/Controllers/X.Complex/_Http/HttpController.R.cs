

namespace SBA.Auth.Controllers;

public partial class __HttpController
{
  [HttpPost]
  public async Task<IActionResult> Gets()
  {
    var result = await ClientHttpAuth.Gets<ResponseRecords<ProjectzLookup>>(new()
    {
      Body = new { includes = new List<string>() { "ProjectzLookupBase" } }
    });
    return result.Records.ToExtVMList().Ok();
  }
  // Single, Include
  [HttpPost("{Id:int}")]
  public async Task<IActionResult> Get(int Id, [FromBody] DtoRequestGet dto)
  {
    var result = await ClientHttpAuth.Get<ResponseRecord<ProjectzLookup>>(new()
    {
      Resource = Id.ToString(),
      Body = new { Includes = dto?.Includes ?? null }
    });
    return result.Record.ToExtVMSingle().Ok();
  }

  [HttpGet]
  public async Task<IActionResult> GetsLookup()
  {
    var result = await ClientHttpAuth.GetsLookup<ResponseRecord<Dictionary<string, string>>>();
    return result.Record.ToExtVMSingle().Ok();
  }
  [HttpPost]
  public async Task<IActionResult> GetsByIds([FromBody] DtoRequestGetByIds<string> req)
  {
    var result = await ClientHttpAuth.GetsByIds<ResponseRecords<ProjectzLookup>>(new()
    {
      Body = new { req.Ids}
    });
    return result.Ok();
  }
  [HttpPost]
  public async Task<IActionResult> GetsByIdsLookup([FromBody] DtoRequestGetByIds<string> req)
  {
    var result = await ClientHttpAuth.GetsByIdsLookup<ResponseRecord<Dictionary<string, string>>>(new()
    {
      Body = new { req.Ids}
    });
    return result.Record.Ok();
  }

  [HttpPost]
  public async Task<IActionResult> GetsPaginate(DtoRequestPage<DtoSearch?> req)
  {
    var result = await ClientHttpAuth.GetsPaginate<VMPaginate<ProjectzLookup>>(new()
    {
      Body = new
      {
        req.Filter,
        req.Includes,
        req.PageNo,
        req.PageSize
      }
    });
    return result.Ok();
  }

  [HttpPost]
  public async Task<IActionResult> GetsPaginateOptions(DtoRequestPage<DtoSearch?> req)
  {
    var result = await ClientHttpAuth.GetsPaginateOptions<VMPaginate<DtoSelect>>(new()
    {
      Body = new
      {
        req.Filter,
        req.Includes,
        req.PageNo,
        req.PageSize
      }
    });
    return result.Ok();
  }
}