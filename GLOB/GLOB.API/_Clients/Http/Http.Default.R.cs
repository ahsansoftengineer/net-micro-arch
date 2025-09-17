namespace GLOB.API.Clientz;

public partial class API_Client_Http
{
  public async Task<T?> Get<T>(HttpParam? param = null)
  {
    if (param == null) param = new();
    param.Action = EP.Get;
    return  await Client.Post<T?>(param);
  }
  public async Task<T?> Gets<T>(HttpParam? param = null)
  {
    if (param == null) param = new();
    param.Action = EP.Gets;
    return  await Client.Post<T?>(param);
  }
  public async Task<T?> GetsLookup<T>(HttpParam? param = null)
  {
    if (param == null) param = new();
    param.Action = EP.GetsLookup;
    return  await Client.Get<T?>(param);
  }
  public async Task<T?> GetsByIds<T>(HttpParam? param = null)
  {
    if (param == null) param = new();
    param.Action = EP.GetsByIds;
    return  await Client.Post<T?>(param);
  }
  public async Task<T?> GetsByIdsLookup<T>(HttpParam param)
  {
    param.Action = EP.GetsByIdsLookup;
    return  await Client.Post<T?>(param);
  }
  public async Task<T?> GetsPaginate<T>(HttpParam param)
  {
    param.Action = EP.GetsPaginate;
    return  await Client.Post<T?>(param);
  }
  public async Task<T?> GetsPaginateOptions<T>(HttpParam param)
  {
    param.Action = EP.GetsPaginateOptions;
    return  await Client.Post<T?>(param);
  }

}
// public async Task<IActionResult> Gets()
// {
//   var result = await ClientHttpAuth.Gets<ResponseRecords<ProjectzLookup>>(new () {
//     Action =  EP.Gets,
//     Body =  new { includes = new List<string>() { "ProjectzLookupBase"} }
//   });
//   var resultz =  result.Records;
//   return resultz.Ok();
// }