using GLOB.API.Clientz;
using RabbitMQ.Client;

namespace SBA.Projectz.Clientz;

public class Projectz_RMQ_Pub : API_RMQ_Pub
{
  public Projectz_RMQ_Pub(IServiceProvider sp) : base(sp)
  {
    this.Init(null);
  }
  protected override void Init(Action<IModel> action = null)
  {
    base.Init((channel) =>
    {
      channel.ExchangeDeclare(
        exchange: "sba.direct",
        type: ExchangeType.Fanout
      );
    });
  }
}