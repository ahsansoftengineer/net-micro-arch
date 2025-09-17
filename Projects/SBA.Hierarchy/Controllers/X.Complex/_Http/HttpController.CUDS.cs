using GLOB.API.Clientz;


namespace SBA.Auth.Controllers;

public partial class __HttpController : API_1_InjectorController<__HttpController>
{
  public readonly API_Client_Http ClientHttpAuth;
  public __HttpController(IServiceProvider sp) : base(sp)
  {
    // string  gatewayUrl = _config.GetValueStr("URLzGateway"); 
    // ClientHttpAuth = new API_Client_Http(_Option_App.Option_Http.Auth, PrefixHttp.Auth, Controllerz.ProjectzLookup);
    ClientHttpAuth = sp.GetSrvc<UOW_API_Httpz>().ClientHttpAuth;
  }
 
  // [HttpPost]
  // public async Task<IActionResult> Add([FromBody] RegisterDto model)
  // {
  // }
  // [HttpPut("{Id}")]
  // public async Task<IActionResult> Update(string Id, [FromBody] UpdateUserDto data)
  // {
  // }

  [HttpDelete("{Id}")]
  public async Task<IActionResult> Delete(string Id)
  {
    var result = await ClientHttpAuth.Delete(new()
    {
      Resource = Id,
    });
    return result.Ok();
  }
  [HttpPatch("{Id}")]
  public async Task<IActionResult> UpdateStatus(string Id, [FromBody] DtoRequestStatus dto)
  {
    var result = await ClientHttpAuth.Status<ResponseRecord<ProjectzLookup>>(new()
    {
      Resource = Id,
      Body = dto 
    });
    return result.Ok();
  }
}