using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GLOB.API.Config.Extz;

public static partial class ExtsAPIConfig
{

  public static IEnumerable<ErrorModel> ToExtValidationError(this ModelStateDictionary modelState)
  {
    return modelState
      .Where(m => m.Value.Errors.Count > 0)
      .Select(m => new ErrorModel
      {
        Field = m.Key,
        Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToArray(),
      });
  }
}
public class ErrorModel
{
  public string Field { get; set; }
  public IEnumerable<string> Errors { get; set; }
}