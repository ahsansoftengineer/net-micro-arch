using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace GLOB.API.Clientz;

public class MsgBusSubBaseFactory: BackgroundService
{
  protected readonly IConfiguration _config;
  protected readonly IServiceScopeFactory _scopeFactory;
  protected readonly Option_RabbitMQ _option_RabbitMQ;
  protected IConnection _connection;
  protected IModel _channel;
  protected string _queueName;

  public MsgBusSubBaseFactory(IServiceProvider sp)
  {
    _config = sp.GetSrvc<IConfiguration>();
    _scopeFactory = sp.GetSrvc<IServiceScopeFactory>();
    _option_RabbitMQ = sp.GetSrvc<IOptions<Option_App>>().Value.Clients.RabbitMQz;
    // InitRabbitMQ("trigger", ExchangeType.Fanout);
  }

  protected void InitRabbitMQ(string route = "route-default", string exchange = "sba", string exchangeType = ExchangeType.Direct)
  {
    var factory = new ConnectionFactory()
    {
      HostName = _option_RabbitMQ.HostName,
      Port = _option_RabbitMQ.Port,
    };

    _connection = factory.CreateConnection();
    _channel = _connection.CreateModel();
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
    _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

    "Listening on the Message Bus...".Print("Rabbit MQ");
  }

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    "ExecuteAsync not Implemented".Print("Rabbit MQ");
    return Task.CompletedTask;
  }

  private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
  {
    "Connection Subs was shut down. Rahul".Print("Rabbit MQ");
  }
  public override void Dispose()
  {
    if (_channel != null && _channel.IsOpen)
    {
      _channel.Close();
      _channel.Dispose();
    }

    if (_connection != null && _connection.IsOpen)
    {
      _connection.Close();
      _connection.Dispose();
    }
    base.Dispose();
  }
}