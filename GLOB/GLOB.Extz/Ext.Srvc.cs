namespace GLOB.Extz;

public static partial class Exts
{
  public static T GetSrvc<T>(this IServiceProvider sp)
    where T : class
  {
    try
    {
      return sp.GetRequiredService<T>();
    }
    catch (Exception ex)
    {
      Print();
      $"Please Regiseter Service in DI {typeof(T).Name}".Print("DI");
      ex.Print();
      return null;
    }
  }
  public static T GetSrvc<T>(this IApplicationBuilder app)
    where T : notnull
  {
    return app.ApplicationServices.GetRequiredService<T>();
  }
}