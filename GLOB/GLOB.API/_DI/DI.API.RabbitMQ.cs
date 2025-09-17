using GLOB.API.Clientz;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace GLOB.API.DI;

public static partial class DI_API
{
  public static void Add_API_RabbitMQ(this IServiceCollection srvc, IConfiguration config)
  {
    srvc.AddSingleton<ConnectionFactory>(sp =>
    {
        var optz = sp.GetRequiredService<IOptions<Option_App>>().Value;
        var option = optz.Clients.RabbitMQz;

        var env = optz.DOTNET_ENVIRONMENT ?? "Production";

        var factory = new ConnectionFactory
        {
            HostName = option.HostName,
            Port = option.Port,
        };

        // ✅ If NOT Development → set credentials (Production/K8S)
        if (!env.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            factory.VirtualHost = option.VirtualHost ?? "/";
            factory.UserName = option.UserName;   // admin (from appsettings.K8S.json)
            factory.Password = option.Password;   // admin123
        }
        // ✅ If Development → rely on guest/guest defaults

        return factory;
    });

    srvc.AddSingleton<IConnection>(sp =>
    {
      var factory = sp.GetSrvc<ConnectionFactory>();
      IConnection conn = factory.CreateConnection();
      conn.ConnectionShutdown += (_, e) =>
      {
        ("Singleton connection shutdown: " + e.ReplyText).Print("Rabbit MQ");
      };
      return conn;
    });

  }
}