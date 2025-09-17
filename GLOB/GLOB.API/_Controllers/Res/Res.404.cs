using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GLOB.API.Staticz;

public static partial class _Res
{
  public static ObjectResult NotFoundId(string id)
  {
    return NotFoundId("Id", id);
  }

  public static ObjectResult NotFoundId(int id)
  {
    return NotFoundId("Id", id.ToString());
  }

  public static ObjectResult NotFoundId(string key, int id)
  {
    return NotFoundId(key, id.ToString());
  }

  public static ObjectResult NotFoundId(string key, string id)
  {
    var modelState = new ModelStateDictionary();
    modelState.AddModelError(key, $"Invalid {key} {id} does not exsist");

    var errors = modelState.ToExtValidationError();
    return new NotFoundObjectResult(new
    {
        Errors = errors,
        Message = "Bad Request, Validation Failed"
    });
  }
}
