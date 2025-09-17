using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace SBA.Projectz.Clientz;

public class RMQ_Sub_Lookup_Create : BackgroundService
{
  private Projectz_RMQ_Sub _sub;
  public RMQ_Sub_Lookup_Create(Projectz_RMQ_Sub sub) : base()
  {
    _sub = sub;
  }

  protected override async Task ExecuteAsync(CancellationToken token)
  {
    token.ThrowIfCancellationRequested();
    await _sub.QueueBindAndConsume("sba.topic", "auth.lookup.create", ProcessEvent);
  }
  public async Task ProcessEvent(BasicDeliverEventArgs ea)
  {
    try
    {
      var body = ea.Body;
      var message = Encoding.UTF8.GetString(body.ToArray());

      using var scope = _sub._scopeFactory.CreateScope();
      using var uow = scope.ServiceProvider.GetRequiredService<IUOW_Infra>();

      var dto = JsonConvert.DeserializeObject<ProjectzLookupDtoCreate>(message);

      if (uow.ProjectzLookupBases.AnyId(dto?.ProjectzLookupBaseId ?? 0))
      {
        ProjectzLookup model = new()
        {
          Name = dto.Name,
          Code = dto.Code,
          Desc = dto.Desc,
          ProjectzLookupBaseId = dto.ProjectzLookupBaseId,
        };
        await uow.ProjectzLookups.Add(model);
        await uow.Save();
        "ProjectzLookup Created Successfully".Print();
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