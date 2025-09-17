
using GLOB.Common.API;

namespace GLOB.API.Controllers.Base;
// Single, List, Group
public abstract partial class API_2_RDS_Controller<TController, TEntity>
{

  // Single, Include
  [HttpPost("{Id:int}")]
  public async Task<IActionResult> Get(int Id, [FromBody] DtoRequestGet dto)
  {
    
    return await _repo.ToActionGet(Id, dto.Includes);
  }

  // List, Filter, Include
  [HttpPost] //[NoCache]
  public async Task<IActionResult> Gets([FromBody] DtoRequestGet dto)
  {
    return await _repo.ToActionGets(dto.Includes);
  }
  // List, Group
  [HttpGet] //[NoCache]
  public async Task<IActionResult> GetsLookup()
  {
    return await _repo.ToActionGetsLookup();
  }

  // List, Filter By Ids
  [HttpPost] //[NoCache]
  public async Task<IActionResult> GetsByIds([FromBody] DtoRequestGetByIds dto)
  {
    return await _repo.ToActionGetsByIds(dto.Ids);
  }

  // List, Group, Filter By Ids
  [HttpPost] //[NoCache]
  public async Task<IActionResult> GetsByIdsLookup([FromBody] DtoRequestGetByIds dto)
  {
    return await _repo.ToActionGetsByIdsLookup(dto.Ids);
  }
  [HttpPost] //[NoCache]
  public async Task<IActionResult> GetsCSV([FromBody] DtoRequestGet dto)
  {
    try
    {
      var rawData = await _repo.Gets(Include: dto.Includes);
      return rawData.ToCsvFileResult(null, (sb) =>
      {
        sb.Replace("Id", "ID");
      });
    }
    catch (Exception ex)
    {
      return new BadRequestObjectResult(ex.Message);
    }
  }
}