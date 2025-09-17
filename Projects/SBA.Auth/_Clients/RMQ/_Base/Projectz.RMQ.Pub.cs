using GLOB.API.Clientz;
using RabbitMQ.Client;

namespace SBA.Projectz.Clientz;

public class Projectz_RMQ_Pub : API_RMQ_Pub
{
  public Projectz_RMQ_Pub(IServiceProvider sp) : base(sp)
  {
    ExchangeDeclare();
  }
  protected override void ExchangeDeclare(Action<IModel> action = null)
  {
    base.ExchangeDeclare((channel) =>
    {
     
    });
  }
}