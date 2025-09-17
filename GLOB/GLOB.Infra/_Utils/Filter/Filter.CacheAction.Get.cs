using System.Net;
using GLOB.Infra.Data.Redisz;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GLOB.Infra.Utils.MIddlewarez;

public class FilterCacheActionGet : IAsyncActionFilter
{
  private readonly RedisCacheService _cache;

  public FilterCacheActionGet(IServiceProvider sp)
  {
    _cache = sp.GetSrvc<RedisCacheService>();
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {

    var route = context.RouteData.Values; // controller, action, Id
    string Id = (string)route["Id"];
    if (Id == null)
    {
      await next();
      return;
    }

    ControllerActionDescriptor? descriptor;
    bool flowControl = FilterCacheActionSave.AllowToContinue(context, out descriptor, "Get");
    if (!flowControl)
    {
      await next();
      return;
    }

    CacheModel cm = new()
    {
      Controller = descriptor.ControllerName,
      Res = Id
    };

    var cached = await _cache.Get(cm);
   
    if (cached != null)
    {
      context.Result = new ObjectResult(cached);
      return;
    }

    var executed = await next();

    if (executed.Result is ObjectResult result)
    {
      HttpStatusCode? status = (HttpStatusCode?)result?.StatusCode;
      if (status == HttpStatusCode.OK)
      {
        cm.Value = result.Value;
        await _cache.Set(cm);
      }
    }
  }
}