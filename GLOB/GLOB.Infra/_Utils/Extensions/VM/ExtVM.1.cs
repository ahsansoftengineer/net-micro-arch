using System.Net;
using System.Text.Json.Serialization;


namespace GLOB.Infra.Extz;

public static partial class ExtResponse
{
  public static ResponseRecord ToExtVMSingle(this object? item)
  {
    return new ResponseRecord 
    {
      Record = item,
      Status = HttpStatusCode.OK
    };
  }
  public static ResponseRecords ToExtVMList(this object? list)
  {
    return new ResponseRecords 
    {
      Records = list,
      Status = HttpStatusCode.OK
    };
  }

  public static async Task<VMPaginate<T>> ToExtVMPage<T>(
    this IQueryable<T> source, VMPaginate<T> p
  )
  {
    if (p.PageNo < 1) p.PageNo = 1;
    if (p.PageSize < 1) p.PageSize = 10;
    if (p.PageSize > 50) p.PageSize = 50;

    p.Count = await source.CountAsync();
    
    var query = source.Skip((p.PageNo - 1) * p.PageSize)
                .Take(p.PageSize);

    p.Records = await query.ToListAsync();
    return p;
  }
}

public class ResponseRecord
{
  public object? Record;
  public HttpStatusCode Status;
}
public class ResponseRecords
{
  public object? Records;
  public HttpStatusCode Status;
}

public class ResponseRecord<T>
  where T: new()
{
  public T? Record  { get; set; } = new();
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public HttpStatusCode Status;
}
public class ResponseRecords<T>
{
  // [JsonPropertyName("records")]
  public List<T> Records { get; set; } = new();

  // [JsonPropertyName("status")]
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public HttpStatusCode Status;
}