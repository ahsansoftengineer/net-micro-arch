namespace GLOB.Extz;

public static partial class Exts
{
  public static string GetValueStr(this IConfiguration configuration, string key)
  {
    string result = configuration.GetValue(key, default(string));
    if (string.IsNullOrEmpty(result)){
      string msg = $"Env has no Value for [{key}]";
      msg.Print();
      return msg;
    }
    return result;
  }
  public static int GetValueInt(this IConfiguration configuration, string key)
  {
    string result = configuration.GetValue(key, default(string));
    if (string.IsNullOrEmpty(result)){
      string msg = $"Env has no Value for [{key}]";
      msg.Print();
      return 0;
    }
    int value;
    int.TryParse(result, out value);
    return value;
  }

  public static string GetWebUrl(this IConfiguration configuration)
  {
    string hostName = configuration.GetValueStr("ASPNETCORE_URLS");
    string prefix = configuration.GetValueStr("ASPNETCORE_ROUTE_PREFIX");
    return $"{hostName}/{prefix}";
  }
}