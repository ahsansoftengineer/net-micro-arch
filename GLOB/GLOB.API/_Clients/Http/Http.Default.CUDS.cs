namespace GLOB.API.Clientz;

public partial class API_Client_Http
{
  public readonly API_HttpBase Client;

  public API_Client_Http(IServiceProvider sp, string host, string srvc, string controller)
  {
    Client = new API_HttpBase(sp, host, srvc, controller);
  }

  public async Task<T?> Add<T>(HttpParam param)
  {
    param.Action = EP.Add;
    return await Client.Post<T>(param);
  }

  public async Task<T?> Status<T>(HttpParam param)
  {
    param.Action = EP.Status;
    return await Client.Patch<T>(param);
  }

  public async Task<T?> Update<T>(HttpParam param)
  {
    param.Action = EP.Update;
    return await Client.Put<T>(param);
  }

  public async Task<object> Delete(HttpParam param)
  {
    param.Action = EP.Delete;
    return await Client.Delete(param);
  }
}
// public readonly API_Client_Http ClientHttpAuth;
// public AuthLookupBaseHttpController(IServiceProvider srvc): base(srvc) 
// {
//   string  gatewayUrl = _config.GetValueStr("URLzGateway"); 
//   ClientHttpAuth = new API_Client_Http(gatewayUrl, PrefixHttp.Auth, Controllerz.ProjectzLookup) ;
// }
// [HttpPost]
// public async Task<IActionResult> Gets()
// {
//   // string gatewayUrl = _config.GetValueStr("URLzGateway"); 
//   var result = await ClientHttpAuth.Gets<ResponseRecords<ProjectzLookup>>(
//     new () {
//       // Host = gatewayUrl,
//       // Srvc = PrefixHttp.Auth,
//       // Controller = Controllerz.Lookup,
//       Action =  EP.Gets,
//       // Resource = "1",
//       // Query = new { Id = 101 },
//       Body =  new {} //new  { includes = new List<string>() { "_projectz-lookup-base"} }

//     }
//   );
//   var resultz =  result.Records;
//   return resultz.Ok();
// }