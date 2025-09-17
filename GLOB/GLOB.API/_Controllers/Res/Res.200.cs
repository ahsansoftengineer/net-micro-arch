using System.Net;

namespace GLOB.API.Staticz;

public static partial class _Res
{
  public static OkObjectResult Ok(this object obj)
  {
    return new OkObjectResult(obj);
  }
  public static ObjectResult Ok(this string msg)
  {
    return Ok(new OkMsg { Message = msg, Status = HttpStatusCode.OK });
  }
  public static ObjectResult CatchException(this Exception ex, string methodName)
  {
    $"Something went wrong in the {methodName}".Print();
    return new ObjectResult("Internal Server Error, Please try again later") { StatusCode = 500 };
  }

  public static ObjectResult CreatedAtAction(ControllerBase controller, string location, IEntityAlpha entity)
  {
    return controller.CreatedAtAction(location, new { entity.Id }, entity);
  }

  public static ObjectResult InvalidEnums(string status)
  {
    return new NotFoundObjectResult($"Invalid enum {status} not updated the record");
  }

  public static ObjectResult NotAuthorised()
  {
    return new BadRequestObjectResult($"You don't have permission to perform the action");
  }
}
public class OkMsg
{
  public string Message  { get; set; }
  public HttpStatusCode Status { get; set; }
}