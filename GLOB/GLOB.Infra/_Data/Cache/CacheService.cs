using LinqKit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace GLOB.Infra.Data.Redisz;

public class RedisCacheService
{
  private readonly IDistributedCache _cache;
  private readonly IConnectionMultiplexer _redis;
  private readonly IServer _server;
  private readonly IConfiguration _config;
  private readonly string prefix;

  public RedisCacheService(IServiceProvider sp)
  {
    _cache = sp.GetSrvc<IDistributedCache>();
    _redis = sp.GetSrvc<IConnectionMultiplexer>(); ;
    _config = sp.GetSrvc<IConfiguration>();
    _server = _redis.GetServer(_redis.GetEndPoints().First());

    prefix = _config.GetValueStr("ASPNETCORE_ROUTE_PREFIX").Replace("/", "-");
    prefix.Print("Redis");
  }
  public async Task Set(CacheModel cm)
  {
    string Key = MrgKey(cm);
    var options = new DistributedCacheEntryOptions();
    if (cm?.Duration != null)
      options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cm.Duration ?? 0);

    var json = JsonConvert.SerializeObject(cm.Value);
    await _cache.SetStringAsync(Key, json, options);
  }

  public async Task Sets(CacheModel cm)
  {
    string Key = MrgKey(cm);
    var options = new DistributedCacheEntryOptions();
    if (cm?.Duration != null)
      options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cm.Duration ?? 0);
    var items = (List<dynamic>)cm.Value;
    if (items.Count > 0)
    {
      await _cache.SetStringAsync(Key, "All", options);
      items.ForEach(async (item) =>
      {
        string json = JsonConvert.SerializeObject(item);
        string key = Key.Replace("*", item.Id);
        await _cache.SetStringAsync(key, json, options);
      });
    }
  }
  

  public async Task<Object> Get(CacheModel cm)
  {
    string? Key = MrgKey(cm);
    if (Key == null) return null;
    var json = await _cache.GetStringAsync(Key);
    if (json != null && !string.IsNullOrEmpty(json))
    {
      $"{Key}".Print("Cache");
      return JsonConvert.DeserializeObject(json);
    }
    return json ?? default;
  }
  public async Task<Object> Gets(CacheModel cm)
  {
    string? Key = MrgKey(cm);
    var hasList = await _cache.GetStringAsync(Key);
    if (hasList == null || string.IsNullOrEmpty(hasList))
      return null;

    if (hasList == "true")
    {
      var list = _server.Keys(pattern: Key.Replace("All", "*"));
      list.ForEach((item) =>
      {
        item.Print("Cache");
      });
      // var parsedList = JsonConvert.DeserializeObject<List<object>>(list);
      Key.Print("Cache");
      // return JsonConvert.DeserializeObject();
      return null;
    }
    return null;
  }
  public async Task Remove(CacheModel cm)
  {
    string? Key = MrgKey(cm);
    if (Key != null)
      await _cache.RemoveAsync(Key);
  }

  public async Task RemoveAll(CacheModel cm)
  {
    string Key = MrgKey(cm);
    var keys = _server.Keys(pattern: Key);
    foreach (var item in keys)
    {
      await _cache.RemoveAsync(item);
    }
  }
  public string? MrgKey(CacheModel? cm)
  {
    if (cm == null || cm?.Controller == null || cm?.Res == null) return null;
    return $"{cm?.Prefix ?? prefix}:{cm?.Controller ?? "Controller"}-{cm?.EP ?? "EP"}:{cm?.Res ?? "*"}";
  }
}

public class CacheModel
{
  public string? Prefix { get; set; }
  public string? Controller { get; set; }
  public string? EP { get; set; }
  public string? Res { get; set; }
  public int? Duration { get; set; }
  public object? Value { get; set; }
}