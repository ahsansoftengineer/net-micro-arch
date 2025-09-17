using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GLOB.API.Clientz;

public class API_RMQ_Sub : IDisposable
{
  public readonly IConfiguration _config;
  public readonly IServiceScopeFactory _scopeFactory;
  public readonly Option_RabbitMQ _option_RabbitMQ;
  public readonly IConnection _connection;
  public readonly IModel _channel;

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
  public virtual Task QueueBindAndConsume(string exchange, string route, Func<BasicDeliverEventArgs, Task> handler)
  {
    string queueName = _channel.QueueDeclare().QueueName;

    _channel.QueueBind(
      queue: queueName,
      exchange: exchange,
      routingKey: route
    );

    var consumer = new EventingBasicConsumer(_channel);
    consumer.Received += async (ModuleHandle, ea) =>
    {
      "Message Recieved".Print("Rabbit MQ");
      await handler(ea);
    };
    
    _channel.BasicConsume(
      queue: queueName,
      autoAck: true,
      consumer: consumer
    );
    return Task.CompletedTask;
  }

  private void Shutdown(object? sender, ShutdownEventArgs e)
  {
    "Connection Subs was shut down. Rahul".Print("Rabbit MQ");
  }
  public void Dispose()
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
  }
}