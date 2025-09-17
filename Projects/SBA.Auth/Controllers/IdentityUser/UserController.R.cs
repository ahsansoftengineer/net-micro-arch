
using GLOB.Domain.Model.Auth;


namespace SBA.Auth.Controllers;

public partial class UserController 
{
  [HttpPost]
  public async Task<IActionResult> Gets()
  {
    var users = _repo.ToList();
    var result = _mapper.Map<List<InfraUserDtoRead>>(users).ToExtVMList();
    return Ok(result);
  }
  [HttpPost("{Id}")]
  public async Task<IActionResult> Get(string Id)
  {
    var data = _repo.FirstOrDefault(x => x.Id == Id);
    var result = _mapper.Map<InfraUserDtoRead>(data).ToExtVMSingle();
    return Ok(result);
  }
  // List, Group
  [HttpGet]
  public async Task<IActionResult> GetsLookup()
  {
    var result = await _repo.Select(x => new { x.Id, x.Name })
        .ToDictionaryAsync(x => x.Id, y =>  y.Name);
    return result.ToExtVMSingle().Ok();
  }

  // List, Filter By Ids
  [HttpPost]
  public async Task<IActionResult> GetsByIds([FromBody] DtoRequestGetByIds<string> dto)
  {
   
    var list = await _repo.Where(x => dto.Ids.Contains(x.Id)).ToListAsync();
    var result = _mapper.Map<List<InfraUserDtoRead>>(list).ToExtVMList();
    return Ok(result);
  
  }
  [HttpPost]
  public async Task<IActionResult> GetsByIdsLookup([FromBody] DtoRequestGetByIds<string> dto)
  {
   
    var list = await _repo
      .Select(x => new { x.Id, x.Name })
      .Where((x) => dto.Ids.Contains(x.Id))
        .ToDictionaryAsync(x => x.Id, y =>  y.Name);
    return list.ToExtVMSingle().Ok();
   
  }
  
  [HttpPost]
  public async Task<IActionResult> GetsPaginate(DtoRequestPage<DtoSearch?> dto)
  {
    var query = _repo
      .ToExtQueryFilter(dto.Filter)
      .ToExtQueryOrderBy(dto.Sort)
      .Select(x => new {
        x.Id,
        x.Name, 
        x.Email,
        x.PhoneNumber,
        x.Status,
        x.CreatedAt,
        x.UpdatedAt
      });
   
    var result = await query.ToExtPageReq(dto);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> GetsPaginateOptions(DtoRequestPage<DtoSearch?> dto)
  {
    var list = await _repo.ToExtVMPageOptionsNoTrack<InfraUser, string>(dto);
    return Ok(list);
  }
}