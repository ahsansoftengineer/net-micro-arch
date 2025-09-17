
namespace SBA.Projectz.Srvc;

public class SrvcInfo
{
  public string date = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
  public void SendEmail(object data)
  {
    $"Sending Email...: Short Running Job {date}".Print("Job");
  }
  public void SendNotification(object data)
  {
    $"Sending Notification...: Short Running Job {date}".Print("Job");
  }
  public void SendSMS(object data)
  {
    $"Sending SMS...: Short Running Job {date}".Print("Job");
  }
  
}