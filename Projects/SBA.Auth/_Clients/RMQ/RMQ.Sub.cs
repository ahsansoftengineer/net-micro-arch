using System.Text;
using GLOB.API.Clientz;
using GLOB.API.Config.Optionz;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SBA.Projectz.Clientz;

public class Projectz_RMQ_Sub : API_RMQ_Sub
{
  public Projectz_RMQ_Sub(IServiceProvider sp) : base(sp)
  {
    ExchangeDeclare();
  }
  protected override void ExchangeDeclare(Action<IModel> action = null)
  {
    base.ExchangeDeclare((channel) =>
    {
      channel.ExchangeDeclare(
        exchange: "sba.direct",
        type: ExchangeType.Direct
      );
    });
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
    string queueName = base.QueueBind("sba.direct", "lookup.create");
    _channel.BasicConsume(
      queue: queueName,
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