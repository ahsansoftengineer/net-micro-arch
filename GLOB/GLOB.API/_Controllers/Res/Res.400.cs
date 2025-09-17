using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GLOB.API.Staticz;

public static partial class _Res
{
  public static ObjectResult BadRequestModel(this ModelStateDictionary modelState)
  {
    foreach (var ms in modelState)
    {
      $"{ms.Key} :\t {ms.Value}".Print("Validation");;
    }

    if (!modelState.Any(x => x.Key == "Message"))
      modelState.AddModelError("Message", "Bad Request 400");

    return new BadRequestObjectResult(modelState);
  }

  public static ObjectResult BadRequestModel(this IdentityError error, ModelStateDictionary modelState = null)
  {
    modelState ??= new ModelStateDictionary();
    modelState.AddModelError(error.Code, error.Description);
    return BadRequestModel(modelState);
  }
  public static ObjectResult BadRequestModel(this IEnumerable<IdentityError> identityError)
  {
    return BadRequestModel(new ModelStateDictionary(), identityError);
  }
  public static ObjectResult BadRequestModel(this ModelStateDictionary modelState, IEnumerable<IdentityError> identityError)
  {
    foreach (var ms in identityError)
    {
      modelState.AddModelError(ms.Code, ms.Description);
    }
    return BadRequestModel(modelState);
  }
  
  public static ObjectResult BadRequestModel(string Title, string Message)
  {
    var modelState = new ModelStateDictionary();
    modelState.AddModelError(Title, Message);
    return BadRequestModel(modelState);
  }

  public static ObjectResult BadRequestzId(string code, int id)
  {
    return BadRequestzId(code, id.ToString());
  }

  public static ObjectResult BadRequestzId(string code, string id)
  {
    return BadRequestModel(new IdentityError
    {
      Code = code,
      Description = $"Invalid {code} {id}"
    });
  }
}
