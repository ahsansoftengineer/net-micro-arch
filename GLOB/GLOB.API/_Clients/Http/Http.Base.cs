using System.Text.Json;
using System.Text.Json.Serialization;

using System.Net.Http.Json;

namespace GLOB.API.Clientz;

public class API_HttpBase
{
  protected readonly string? _host;
  protected readonly string? _srvc;
  protected readonly string? _controller;

  protected readonly HttpClient _httpClient;

  public API_HttpBase(IServiceProvider sp, string host, string srvc, string controller)
  {
    _httpClient = sp.GetSrvc<HttpClient>();
    _host = host;
    _srvc = srvc;
    _controller = controller;
  }
  public async Task<T?> Get<T>(HttpParam param)
  {
    setDefault(param);
    var response = await _httpClient.GetAsync(param.Url);
    var result = await DeserializeResponse<T>(response);

    return result;
  }

  public async Task<T?> Post<T>(HttpParam param)
  {
    setDefault(param);
    var response = await _httpClient.PostAsJsonAsync(param.Url, param.Body);
    return await DeserializeResponse<T>(response);
  }

  public async Task<T?> Patch<T>(HttpParam param)
  {
    setDefault(param);
    var response = await _httpClient.PatchAsJsonAsync(param.Url, param.Body);
    return await DeserializeResponse<T>(response);
  }

  public async Task<T?> Put<T>(HttpParam param)
  {
    setDefault(param);
    var response = await _httpClient.PutAsJsonAsync(param.Url, param.Body);
    return await DeserializeResponse<T>(response);
  }

  // Not Returning anything
  public async Task<object> Delete(HttpParam param)
  {
    setDefault(param);
    // var response = await _httpClient.DeleteFromJsonAsync<ResponseRecord>(param.Url);
    var request = new HttpRequestMessage(HttpMethod.Delete, param.Url);
    var response = await _httpClient.SendAsync(request);
    return await DeserializeResponse<OkMsg>(response);
  }

  private HttpParam setDefault(HttpParam param)
  {

    if (string.IsNullOrEmpty(param?.Host))
      param.Host = _host ?? "no-host";

    if (string.IsNullOrEmpty(param?.Srvc))
      param.Srvc = _srvc ?? "no-service";

    if (string.IsNullOrEmpty(param?.Controller))
      param.Controller = _controller ?? "no-controller";

    string action = append(param.Action);
    string res = append(param.Resource);

    var query = System.Web.HttpUtility.ParseQueryString(string.Empty);

    if (param.Query != null)
    {
      foreach (var prop in param.Query.GetType().GetProperties())
      {
        var value = prop.GetValue(param.Query)?.ToString();
        if (!string.IsNullOrWhiteSpace(value))
          query[prop.Name] = value;
      }
    }

    string queryString = query.HasKeys() ? $"?{query}" : "";

    param.Url = $"{param.Host}/{param.Srvc}/{param.Controller}{action}{res}{queryString}";

    return param;
  }
  private string append(string? value)
  {
    return string.IsNullOrEmpty(value) ? "" : $"/{value}";
  }

  private static async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
  {
    if (!response.IsSuccessStatusCode)
    {
      try
      {
        // Status: NotFound, Content: {"errors":[{"field":"Id","errors":["Invalid Id 3 does not exsist"]}],"message":"Bad Request, Validation Failed"}
        
      }
      catch(Exception ex){
        ex.Message.Print();
        var content = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Status: {response.StatusCode}, Content: {content}");
      }
    }

    var result = await response.Content.ReadFromJsonAsync<T>(new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true,
      Converters = { new JsonStringEnumConverter() }
    });

    return result;
  }
}

public record HttpParam
{
  public string? Host { get; set; } // http://www.arab.com || environment.API_req
  public string? Srvc { get; set; } // api/Hierarchy/v1 || environment.API_req
  public string? Controller { get; set; } // employee || environment.API_req
  public string Action { get; set; } // get, gets, get-by-id
  public string? Resource { get; set; } //// get-by-id/1

  public object? Query { get; set; } // {id:1, name:Muhammad}
  // public string? Param { get; set; } // ?id=1&name=Muhammad

  public string? Url { get; set; }

  public object? Body { get; set; } // Specific to CREATE, PATCH, DELETE
}
