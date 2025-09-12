using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace GLOB.API.Clientz;

public class API_RMQ_Sub: BackgroundService
{
  protected readonly IConfiguration _config;
  protected readonly IServiceScopeFactory _scopeFactory;
  protected readonly Option_RabbitMQ _option_RabbitMQ;
  protected IConnection _connection;
  protected IModel _channel;

  public API_RMQ_Sub(IServiceProvider sp)
  {
    _config = sp.GetSrvc<IConfiguration>();
    _scopeFactory = sp.GetSrvc<IServiceScopeFactory>();
    _option_RabbitMQ = sp.GetSrvc<IOptions<Option_App>>().Value.Clients.RabbitMQz;

    try
    {
      var factory = new ConnectionFactory
      {
        HostName = _option_RabbitMQ.HostName,
        Port = _option_RabbitMQ.Port,
        // UserName = _option_RabbitMQ.UserName,
        // Password = _option_RabbitMQ.Password
      };
      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
    }
    catch (Exception ex)
    {
      ex.Print("API_RMQ_Sub");
    }
  }
  protected virtual void ExchangeDeclare(Action<IModel> action = null)
  {
    try
    {
      if (action != null)
        action(_channel);
      else
        "No Exchange Declare".Print("API_RMQ_Sub");

      _connection.ConnectionShutdown += Shutdown;
      "Connection Successfull".Print("API_RMQ_Sub");
    }
    catch (Exception ex)
    {
      $"Connection Failed{ex.Message}".Print("API_RMQ_Sub"); ;
    }
  }
  protected virtual string QueueBind(string exchange = "sba", string route = "route-default")
  {
    string queueName = _channel.QueueDeclare().QueueName;

    _channel.QueueBind(
      queue: queueName,
      exchange: exchange,
      routingKey: route
    );
    $"Listening on {exchange} - {route}".Print("Rabbit MQ");
    return queueName;
  }

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    "ExecuteAsync not Overridden".Print("Rabbit MQ");
    return Task.CompletedTask;
  }

  private void Shutdown(object? sender, ShutdownEventArgs e)
  {
    "Connection Subs was shut down. Rahul".Print("Rabbit MQ");
  }
  private override void Dispose()
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