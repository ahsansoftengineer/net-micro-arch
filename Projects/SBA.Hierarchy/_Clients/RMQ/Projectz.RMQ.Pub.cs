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
      channel.ExchangeDeclare(
        exchange: "sba.topic",
        type: ExchangeType.Topic,
        durable: false,   // <-- must match existing
        autoDelete: false,
        arguments: null
      );
      // channel.ExchangeDeclare(
      //   exchange: "sba.fanout",
      //   type: ExchangeType.Fanout
      // );
      // channel.ExchangeDeclare(
      //   exchange: "sba.topic",
      //   type: ExchangeType.Topic
      // );
      // channel.ExchangeDeclare(
      //   exchange: "sba.headers",
      //   type: ExchangeType.Headers
      // );
    });
  }
}