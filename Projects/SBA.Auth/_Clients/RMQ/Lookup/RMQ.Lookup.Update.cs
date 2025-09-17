using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace SBA.Projectz.Clientz;

public class RMQ_Sub_Lookup_Update : BackgroundService
{
  private Projectz_RMQ_Sub _sub;
  public RMQ_Sub_Lookup_Update(Projectz_RMQ_Sub sub) : base()
  {
    _sub = sub;
  }

  protected override async Task ExecuteAsync(CancellationToken token)
  {
    token.ThrowIfCancellationRequested();
    await _sub.QueueBindAndConsume("sba.topic", "auth.lookup.update", ProcessEvent);
  }
  public async Task ProcessEvent(BasicDeliverEventArgs ea)
  {
    try
    {
      var body = ea.Body;
      var message = Encoding.UTF8.GetString(body.ToArray());

      using var scope = _sub._scopeFactory.CreateScope();
      using var uow = scope.ServiceProvider.GetRequiredService<IUOW_Infra>();

      var dto = JsonConvert.DeserializeObject<ProjectzLookup>(message);

      if (uow.ProjectzLookupBases.AnyId(dto?.ProjectzLookupBaseId ?? 0))
      {
        var model = await uow.ProjectzLookups.Get(dto.Id);

        model.Name = dto.Name;
        model.Desc = dto.Desc;
        model.ProjectzLookupBaseId = dto.ProjectzLookupBaseId;
        uow.ProjectzLookups.Update(model);
        await uow.Save();
        "ProjectzLookup Updated Successfully".Print();
      }
      else
        "ProjectzLookupBaseId Does not Exsist".Print();

    }
    catch (Exception ex)
    {
      $"ProjectzLookup not Created {ex.Message}".Print();
    }
  }
}