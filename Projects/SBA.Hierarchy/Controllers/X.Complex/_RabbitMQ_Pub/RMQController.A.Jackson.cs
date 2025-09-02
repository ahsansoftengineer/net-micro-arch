using GLOB.API.Clientz;

using GLOB.Infra.Utils.Attributez;
using SBA.Projectz.Clientz;
namespace SBA.Auth.Controllers;

public partial class __RabbitMQController : API_1_InjectorController<__RabbitMQController>
{
  private readonly MsgBusPub RabbitMQ_Name;
  private readonly API_RabbitMQ_Base_Pubs _rmqPubs;
  private readonly Projectz_RMQ_Pub _rmqPub;
  public RabbitMQRoute Route = null;
  public __RabbitMQController(IServiceProvider sp) : base(sp)
  {
    RabbitMQ_Name = sp.GetSrvc<MsgBusPub>();
    _rmqPubs = sp.GetSrvc<API_RabbitMQ_Base_Pubs>();
    _rmqPub = sp.GetSrvc<Projectz_RMQ_Pub>();
    Route = new RabbitMQRoute(MQ_Exch.Auth, Controllerz.Auth.ProjectzLookup);
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
      RabbitMQ_Name.Publish(data);
      return data.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      // return ex.Ok();
      return $"[Rabbit MQ] Error : {ex.Message}".ToExtVMSingle().Ok();
    }

  }
}