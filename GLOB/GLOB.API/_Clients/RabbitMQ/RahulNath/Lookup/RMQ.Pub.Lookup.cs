using RabbitMQ.Client;

namespace GLOB.API.Clientz;

public class MsgBusLookupPub
{
  public MsgBusPubFactory MsgBus { get; }
  public MsgBusLookupPub(MsgBusPubFactory msgBus)
  {
    MsgBus = msgBus;
    msgBus.Init((channel) =>
    {
      channel.ExchangeDeclare(
        exchange: "sba.direct.lookup",
        type: ExchangeType.Fanout
      );
    });
  }

  public void LookupCUDS(object data)
  {
    MsgBus.Publish(data, (channel, body) =>
    {
      channel.BasicPublish(
        exchange: "sba",
        routingKey: "lookup.create",
        basicProperties: null,
        body
      );
      channel.BasicPublish(
        exchange: "sba",
        routingKey: "lookup.update",
        basicProperties: null,
        body
      );
      //...
    });
  }
}