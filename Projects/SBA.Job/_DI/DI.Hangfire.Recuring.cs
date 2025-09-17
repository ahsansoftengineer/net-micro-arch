using Hangfire;
using SBA.Projectz.Srvc;

public static partial class DI_Projectz
{
  // https://freeformatter.com/cron-expression-generator-quartz.html
  public static void Call_Hangfire_Recuring_Jobs(this IApplicationBuilder app)
  {
    // 0/10 0 0 ? * * * -> Every 10 Secs
    // 0 * * ? * * -> Every Minutes
    // RecurringJob.AddOrUpdate<SrvcInfo>(opt => opt.SendEmail(), "0 * * ? * *");

    SendEmail(new { });
    SendSMS(new { });
    SendNotification(new { });

  }
  // Send Email Every Minute
  public static void SendEmail(object data)
  {
    "--> RecurringJob.AddOrUpdate<SrvcInfo> SendEmail(data) Minutely".Print("Job");
    RecurringJob.AddOrUpdate<SrvcInfo>(
      recurringJobId: "job-notify-send-email",
      methodCall: x => x.SendEmail(data),
      cronExpression:  Cron.Minutely, //"* * * ? * *",
      queue: "queue-short-time",
      options: new RecurringJobOptions
      {
        TimeZone = TimeZoneInfo.Local,
      }
    );
  }

  public static void SendNotification(object data)
  {
    "--> RecurringJob.AddOrUpdate<SrvcInfo> x.SendNotification(data) Hourly".Print("Job");
    RecurringJob.AddOrUpdate<SrvcInfo>(
      recurringJobId: "job-notify-send-notification",
      methodCall: x => x.SendNotification(data),
      cronExpression: Cron.Hourly, //"*/30 * * ? * *", // Every 30 Minutes
      queue: "queue-long-time",
      options: new RecurringJobOptions
      {
        TimeZone = TimeZoneInfo.Local,
      }
    );
  }

  public static void SendSMS(object data)
  {
    "--> RecurringJob.AddOrUpdate<SrvcInfo> x.SendSMS(data) Daily".Print("Job");
    RecurringJob.AddOrUpdate<SrvcInfo>(
      recurringJobId: "job-notify-send-sms",
      methodCall: x => x.SendSMS(data),
      cronExpression: Cron.Daily, // "0 * * ? * *",
      queue: "queue-long-time",
      options: new RecurringJobOptions
      {
        TimeZone = TimeZoneInfo.Local,
      }
    );
  }
}