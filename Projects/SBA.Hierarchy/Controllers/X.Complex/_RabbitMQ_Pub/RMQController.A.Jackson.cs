using GLOB.API.Clientz;

using GLOB.Infra.Utils.Attributez;
using SBA.Projectz.Clientz;
namespace SBA.Auth.Controllers;

public partial class __RabbitMQController : API_1_InjectorController<__RabbitMQController>
{
  private readonly MsgBusPub MsgBusJackson;
  private readonly Projectz_RMQ_Pub _rmqPub;
  public __RabbitMQController(IServiceProvider sp) : base(sp)
  {
    MsgBusJackson = sp.GetSrvc<MsgBusPub>();
    _rmqPub = sp.GetSrvc<Projectz_RMQ_Pub>();
  }

  [HttpPost] [NoCache]
  public async Task<IActionResult> Add([FromBody] ProjectzLookupDtoCreate model)
  {
    try
    {
      var data = new
      {
        model.Name,
        model.Code,
        model.Desc,
        model.ProjectzLookupBaseId,
        Status.Active,
        Event = $"ProjectzLookup_{EP.Add}"
      };
      MsgBusJackson.Publish(data);
      return data.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      // return ex.Ok();
      return $"[Rabbit MQ] Error : {ex.Message}".ToExtVMSingle().Ok();
    }

  }
}