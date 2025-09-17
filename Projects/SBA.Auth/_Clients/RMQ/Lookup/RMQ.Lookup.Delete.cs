using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace SBA.Projectz.Clientz;

public class RMQ_Sub_Lookup_Delete : BackgroundService
{
  private Projectz_RMQ_Sub _sub;
  public RMQ_Sub_Lookup_Delete(Projectz_RMQ_Sub sub) : base()
  {
    _sub = sub;
  }

  protected override async Task ExecuteAsync(CancellationToken token)
  {
    token.ThrowIfCancellationRequested();
    await _sub.QueueBindAndConsume("sba.topic", "auth.lookup.delete", ProcessEvent);
  }
  public async Task ProcessEvent(BasicDeliverEventArgs ea)
  {
    try
    {
      var body = ea.Body;
      var message = Encoding.UTF8.GetString(body.ToArray());
      var dto = JsonConvert.DeserializeObject<DtoRequestDelete>(message);

      using var scope = _sub._scopeFactory.CreateScope();
      using var uow = scope.ServiceProvider.GetRequiredService<IUOW_Infra>();

      var model = await uow.ProjectzLookups.Get(dto.Id ?? 0);

      model.Status = Status.Delete;
      uow.ProjectzLookups.Update(model);
      await uow.Save();
      "ProjectzLookup Deleted Successfully".Print();

    }
    catch (Exception ex)
    {
      $"ProjectzLookup not Created {ex.Message}".Print();
    }
  }
}