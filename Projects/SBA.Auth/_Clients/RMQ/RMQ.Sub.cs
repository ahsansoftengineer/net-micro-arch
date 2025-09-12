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
        exchange: "sba.direct",
        type: ExchangeType.Direct
      );
    });
  }

  protected override async Task ExecuteAsync(CancellationToken token)
  {
    token.ThrowIfCancellationRequested();
    await QueueBindAndConsume("sba.direct", "lookup.create", ProcessEvent);
  }
  public async Task ProcessEvent(BasicDeliverEventArgs ea)
  {
    try
    {
      var body = ea.Body;
      var message = Encoding.UTF8.GetString(body.ToArray());

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