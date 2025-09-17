

using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SBA.Projectz.Srvc;

namespace SBA.Job.Controllers;

public class InfoController : API_1_InjectorController<InfoController>
{
  public InfoController(IServiceProvider srvcProvider) : base(srvcProvider)
  {
  }


  // Fire when need
  [HttpPost]
  public async Task<IActionResult> BulkCreateProjectzLookup([FromBody] bool isSuccess)
  {
    if (!isSuccess)
      return "Failed to Send Email".Ok();
    "--> BackgroundJob.Enqueue<SrvcProjectzLookup>(x => x.BulkCreate());".Print("Job");
    BackgroundJob.Enqueue<SrvcProjectzLookup>(x => x.BulkCreate());
    return "Email Sent Successfully".Ok();
  }

  // Fire when need
  [HttpPost]
  public async Task<IActionResult> BulkUpdateProjectzLookup([FromBody] bool isSuccess)
  {
    if (!isSuccess)
      return "Failed to Send Email".Ok();

    "--> BackgroundJob.Enqueue<SrvcProjectzLookup>(x => x.BulkUpdate());".Print("Job");
    BackgroundJob.Enqueue<SrvcProjectzLookup>(x => x.BulkUpdate());
    return "Email Sent Successfully".Ok();
  }
  
  // Fire and Forget
  [HttpPost]
  public async Task<IActionResult> UpdateDBProjectzLookup([FromBody] bool isSuccess)
  {
    if (!isSuccess)
      return "Failed to Send Email".Ok();

      "--> RecurringJob.AddOrUpdate<SrvcProjectzLookup> x.UpdateDatabase() Minutly".Print("Job");
     RecurringJob.AddOrUpdate<SrvcProjectzLookup>(
      recurringJobId: "job-notify-send-email",
      methodCall: x => x.UpdateDatabase(),
      cronExpression: Cron.Minutely,
      queue: "queue-daily",
      options: new RecurringJobOptions
      {
        TimeZone = TimeZoneInfo.Local,
      }
    );
    return "Email Sent Successfully".Ok();
  }
}