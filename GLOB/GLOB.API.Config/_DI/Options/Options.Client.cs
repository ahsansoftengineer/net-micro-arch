namespace GLOB.API.Config.Optionz;

public class Option_Host
{
  public static string SectionName = "Http_Host";
  public string Gateway { get; set; } = string.Empty;
  public string Job { get; set; } = string.Empty;
  public string Auth { get; set; } = string.Empty;
  public string Hierarchy { get; set; } = string.Empty;
}

public class Option_RabbitMQ
{
  public static string SectionName = "RabbitMQz";
  public string Uri { get; set; } = string.Empty;
  public int Port { get; set; }
  public string HostName { get; set; } = string.Empty;
  public string VirtualHost { get; set; } = "/";
  public string UserName { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
}

public class Option_Clients
{
  public static string SectionName = "Clients";
  public Option_RabbitMQ RabbitMQz { get; set; }
  public Option_Host Http_Host { get; set; } = new Option_Host();
}
    //   // Uri = "",
    //   // Port = 5672,
    //   HostName = hostName,
    //   VirtualHost = virtualHost,
    //   UserName = user,
    //   Password = password