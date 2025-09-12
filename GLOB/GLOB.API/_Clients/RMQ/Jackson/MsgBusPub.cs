using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace GLOB.API.Clientz;
public class MsgBusPub : IDisposable
{
  protected readonly Option_RabbitMQ _option_RabbitMQ;
  protected IConnection _connection;
  protected IModel _channel;
  public MsgBusPub(IServiceProvider sp)
  {
    _option_RabbitMQ = sp.GetSrvc<IOptions<Option_App>>().Value.Clients.RabbitMQz;
    Init();
  }
  public void Publish(object data)
  {
    try
    {
      var message = JsonConvert.SerializeObject(data);
      if (_connection.IsOpen)
      {
        "Connection Open, sending message...".Print("Rabbit MQ");;
        SendMessage(message);
      }
      else
      {
        "Connection Close, not sending".Print("Rabbit MQ");
      }
    }
    catch (Exception ex)
    {
      $"Serialization failed: {ex.Message}".Print("Rabbit MQ");
      ex.StackTrace.Print();
    }
  }
  private void SendMessage(string message)
  {
    var body = Encoding.UTF8.GetBytes(message);
    _channel.BasicPublish(
      exchange: "trigger",
      routingKey: "",
      basicProperties: null,
      body
    );
    $"--> We have send: {message}".Print("Rabbit MQ");;
  }
  public void Init()
  {
    var factory = new ConnectionFactory
    {
      HostName = _option_RabbitMQ.HostName,
      Port = _option_RabbitMQ.Port
    };
    try
    {
      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();

      _channel.ExchangeDeclare(
          exchange: "trigger",
          type: ExchangeType.Fanout
      );
      _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
      "Connection Successfull".Print("Rabbit MQ");;
    }
    catch (Exception ex)
    {
      $"Connection Failed{ex.Message}".Print("Rabbit MQ");;
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
}