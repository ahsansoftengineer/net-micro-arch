using System.Net;
using GLOB.Infra.Data.Redisz;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GLOB.Infra.Utils.MIddlewarez;

public class FilterCacheActionGets : IAsyncActionFilter
{
  private readonly RedisCacheService _cache;

  public FilterCacheActionGets(IServiceProvider sp)
  {
    _cache = sp.GetSrvc<RedisCacheService>();
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    ControllerActionDescriptor? descriptor;
    bool flowControl = FilterCacheActionSave.AllowToContinue(context, out descriptor, "Gets");
    if (!flowControl)
    {
      await next();
      return;
    }
    string controller = descriptor.ControllerName;

    CacheModel cm = new()
    {
      Controller = controller,
      Res = "All"
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