namespace SBA.Projectz.Srvc;

public class SrvcProjectzLookup
{
  public string date => DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
  public void GenerateMerchandise()
  {
    $"GenerateMerchandise: Long Running Job {date}".Print("Job");
  }
  public void BulkCreate()
  {
    $"BulkCreate: Long Running Job {date}".Print("Job");
  }

  public void BulkUpdate()
  {
    $"BulkUpdate: Long Running Job {date}".Print("Job");
  }
  public void SyncData()
  {
    $"SyncData: Long Running Job {date}".Print("Job");
  }
  public void UpdateDatabase()
  {
    $"UpdateDatabase: Long Running Job {date}".Print("Job");
  }
  
}