using System.Text;

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GLOB.API.Clientz;

public class MsgBusLookupSub: MsgBusSubBaseFactory
{
  public MsgBusLookupSub(IServiceProvider sp) : base(sp)
  {
    InitRabbitMQ("lookup.create", "sba", ExchangeType.Fanout);
  }
  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    stoppingToken.ThrowIfCancellationRequested();
    var consumer = new EventingBasicConsumer(_channel);
    consumer.Received += async (ModuleHandle, ea) =>
    {
      "Message Recieved".Print("Rabbit MQ");
      var body = ea.Body;
      var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

      await ProcessEvent(notificationMessage);

    };
    _channel.BasicConsume(
      queue: _queueName,
      autoAck: true,
      consumer: consumer
    );
    return Task.CompletedTask;
  }

  public async Task ProcessEvent(string message)
  {
    try
    {
      using var scope = _scopeFactory.CreateScope();
      using var uow = scope.ServiceProvider.GetRequiredService<IUOW_Infra>();
      var model = JsonConvert.DeserializeObject<ProjectzLookup>(message);

      if (uow.ProjectzLookupBases.AnyId(model?.ProjectzLookupBaseId ?? 0))
      {
        await uow.ProjectzLookups.Add(model);
        await uow.Save();
        "ProjectzLookup Created Successfully".Print();
      }
      else
      {
        "ProjectzLookupBaseId Does not Exsist".Print();
      }

    }
    catch (Exception ex)
    {
      $"ProjectzLookup not Created {ex.Message}".Print();
    }
  }
}