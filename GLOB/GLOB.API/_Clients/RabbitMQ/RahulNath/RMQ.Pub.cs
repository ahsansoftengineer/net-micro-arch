using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace GLOB.API.Clientz;

public class API_RMQ_Pub : IDisposable
{
  protected readonly Option_RabbitMQ _option_RabbitMQ;
  protected readonly IConnection _connection;
  protected readonly IModel _channel;
  public API_RMQ_Pub(IServiceProvider sp)
  {
    _option_RabbitMQ = sp.GetSrvc<IOptions<Option_App>>().Value.Clients.RabbitMQz;

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
    }
    catch (Exception ex)
    {
      ex.Print("API_RMQ_Pub");
    }
  }
  protected virtual void Init(Action<IModel> action = null)
  {
    try
    {
      if (action != null)
        action(_channel);
      else
        "No Exchange Declare".Print("Rabbit MQ");

      _connection.ConnectionShutdown += Shutdown;
      "Connection Successfull".Print("Rabbit MQ");
    }
    catch (Exception ex)
    {
      $"Connection Failed{ex.Message}".Print("Rabbit MQ"); ;
    }
  }
  protected void Shutdown(object? sender, ShutdownEventArgs e)
  {
    "connection Pub was shut down. Jackson".Print("Rabbit MQ");
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

        if (action != null)
        {
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