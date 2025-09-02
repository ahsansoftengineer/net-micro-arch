using GLOB.API.Clientz;
using GLOB.API.Config.Optionz;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace SBA.Projectz.Clientz;

public class Projectz_RMQ_Sub: API_RMQ_Sub
{
  public Projectz_RMQ_Sub(IServiceProvider sp) : base(sp)
  {
    Init("sba", ExchangeType.Direct, "");
  }

  protected void Init(string exchange = "sba", string exchangeType = ExchangeType.Direct, string route = "route-default")
  {
    _channel.ExchangeDeclare(
      exchange: exchange,
      type: exchangeType
    );

    _queueName = _channel.QueueDeclare().QueueName;

    _channel.QueueBind(
      queue: _queueName,
      exchange: exchange,
      routingKey: route
    );
    // _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

    "Listening on the Message Bus...".Print("Rabbit MQ");
  }

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    "ExecuteAsync not Implemented".Print("Rabbit MQ");
    return Task.CompletedTask;
  }
}