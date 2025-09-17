using System.Net;
using System.Reflection;
using GLOB.Infra.Utils.Attributez;
using GLOB.Infra.Data.Redisz;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GLOB.Infra.Utils.MIddlewarez;

public class FilterCacheActionSave : IAsyncActionFilter
{
  private readonly RedisCacheService _cache;

  public FilterCacheActionSave(IServiceProvider sp)
  {
    _cache = sp.GetSrvc<RedisCacheService>();
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    ControllerActionDescriptor? descriptor;
    bool flowControl = AllowToContinue(context, out descriptor);
    if (!flowControl)
    {
      await next();
      return;
    }

    var route = context.RouteData.Values; // controller, action, Id
    string action = (string)route["action"];
    string Id = (string)route["Id"];

    CacheModel cm = new()
    {
      Controller = descriptor.ControllerName,
    };

    if (Id != null)
      cm.Res = Id;

    // Setting Cache By Read
    var executed = await next();

    if (executed.Result is ObjectResult result)
    {
      HttpStatusCode? status = (HttpStatusCode?)result?.StatusCode;

      if (status == HttpStatusCode.OK || status == HttpStatusCode.Created)
      {
        $"Handling Action {action} Status {status} in Cache -->".Print();

        if ("Update, Status".IndexOf(action) != -1)
        {
          cm.Value = result.Value;
          await _cache.Set(cm);
        }
        else if (action == "Delete")
        {
          await _cache.Remove(cm);
        }
        else if (action == "Add")
        {
          var prop = result.Value?.GetType().GetProperty("Id");
          var id = prop?.GetValue(result.Value, null);
          if (id != null)
            cm.Res = id.ToString();

          cm.Value = new
          {
            Record = result.Value,
            Status = HttpStatusCode.OK
          };
          await _cache.Set(cm);
        }
      }
    }
  }
  

  public static bool AllowToContinue(
    ActionExecutingContext context,
    out ControllerActionDescriptor? descriptor,
    string Actions = "Add, Update, Delete, Status"
    )
  {
    descriptor = context.ActionDescriptor as ControllerActionDescriptor;
    if (descriptor == null)
      return false;

    var method = descriptor.MethodInfo;
    var controllerType = context.Controller.GetType();

    bool hasNoCache = method?.GetCustomAttribute<NoCacheAttribute>() != null ||
                      controllerType.GetCustomAttribute<NoCacheAttribute>() != null;

    var cacheAttr = method?.GetCustomAttribute<CacheAttribute>() ??
                    controllerType.GetCustomAttribute<CacheAttribute>();

    if (hasNoCache || cacheAttr == null)
      return false;

    var action = (string)context.RouteData.Values["action"]; // controller, action, Id

    if (action == null || Actions.IndexOf(action) == -1)
      return false;

    return true;
  }
}