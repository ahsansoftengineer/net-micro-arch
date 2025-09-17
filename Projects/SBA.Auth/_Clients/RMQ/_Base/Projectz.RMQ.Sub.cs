using System.Text;
using GLOB.API.Clientz;
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
        exchange: "sba.topic",
        type: ExchangeType.Topic,
        durable: false,   // <-- must match existing
        autoDelete: false,
        arguments: null
      );
    });
  }
  // public async Task ProcessEvent(BasicDeliverEventArgs ea)
  // {
  //   try
  //   {
  //     var body = ea.Body;
  //     var message = Encoding.UTF8.GetString(body.ToArray());

  //     using var scope = _scopeFactory.CreateScope();
  //     using var uow = scope.ServiceProvider.GetRequiredService<IUOW_Infra>();

  //     var model = JsonConvert.DeserializeObject<ProjectzLookup>(message);

  //     //...

  //   }
  //   catch (Exception ex)
  //   {
  //     $"ProjectzLookup not Created {ex.Message}".Print();
  //   }
  // }
}