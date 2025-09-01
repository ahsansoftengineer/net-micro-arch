using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace GLOB.API.Clientz;

public class MsgBusLookupPub : MsgBusPubBaseFactory
{
  public MsgBusLookupPub(IServiceProvider sp) : base(sp)
  {
    InitRabbitMQPub("sba", ExchangeType.Fanout);
  }
  public void LookupCreate(object data)
  {
    base.Publish(data, (channel, body) =>
    {
      channel.BasicPublish(
        exchange: "sba",
        routingKey: "lookup-create",
        basicProperties: null,
        body
      );
    });
  }
  public void LookupUpdate(object data)
  {
    base.Publish(data, (channel, body) =>
    {
      channel.BasicPublish(
        exchange: "sba",
        routingKey: "lookup-update",
        basicProperties: null,
        body
      );
    });
  }
  public void LookupStatus(object data)
  {
    base.Publish(data, (channel, body) =>
    {
      channel.BasicPublish(
        exchange: "sba",
        routingKey: "lookup-update",
        basicProperties: null,
        body
      );
    });
  }
}