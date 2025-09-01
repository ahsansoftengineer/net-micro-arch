using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace GLOB.API.Clientz;

public class MsgBusPubBaseFactory : IDisposable
{
  protected readonly Option_RabbitMQ _option_RabbitMQ;
  protected IConnection _connection;
  protected IModel _channel;
  public MsgBusPubBaseFactory(IServiceProvider sp)
  {
    _option_RabbitMQ = sp.GetSrvc<IOptions<Option_App>>().Value.Clients.RabbitMQz;
  }
  public void InitRabbitMQPub(string exchange, string exchangeType)
  {
    var factory = new ConnectionFactory
    {
      HostName = _option_RabbitMQ.HostName,
      Port = _option_RabbitMQ.Port,
      // UserName = _option_RabbitMQ.UserName,
      // Password = _option_RabbitMQ.Password
    };
    try
    {
      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();

      _channel.ExchangeDeclare(
          exchange: exchange,
          type: exchangeType
      );
      _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
      "Connection Successfull".Print("Rabbit MQ"); ;
    }
    catch (Exception ex)
    {
      $"Connection Failed{ex.Message}".Print("Rabbit MQ"); ;
    }
  }

  private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
  {
    "connection was shut down. Jackson".Print("Rabbit MQ");
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


  public void Publish(object data, Action<IModel, byte[]> action = null)
  {
    try
    {
      var message = JsonConvert.SerializeObject(data);
      if (_connection.IsOpen)
      {
        "Connection Open, sending message...".Print("Rabbit MQ");

        var body = Encoding.UTF8.GetBytes(message);

        if (action != null) {
          action(_channel, body);
          $"We have sent: {message}".Print("Rabbit MQ");
        }
        else
          "Action Handler is Missing".Print("Rabbit MQ");

      }
      else
        "Connection Closed, not sending".Print("Rabbit MQ");
    }
    catch (Exception ex)
    {
      $"Serialization failed: {ex.Message}".Print("Rabbit MQ");
      ex.StackTrace.Print();
    }
  }

}