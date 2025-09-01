using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace GLOB.API.Clientz;
public class MsgBusPubFactory : MsgBusPubBaseFactory
{
  public MsgBusPubFactory(IServiceProvider sp) : base(sp)
  {
    InitRabbitMQPub("trigger", ExchangeType.Fanout);
  }
  public void Publish(object data)
  {
    base.Publish(data, (channel, body) =>
    {
      channel.BasicPublish(
        exchange: "trigger",
        routingKey: "",
        basicProperties: null,
        body
      );
    });
  }
}