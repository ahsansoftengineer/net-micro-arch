using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace GLOB.Common.API;

public static class CsvExportExtensions
{
  public static IActionResult ToCsvFileResult(this object rawData, string? fileName = null, Action<StringBuilder> changeColumnHeader = null)
  {
    var records = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(
      JsonConvert.SerializeObject(rawData, new JsonSerializerSettings
      {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
      }));

    if (records == null || records.Count == 0)
    {
      return new NotFoundObjectResult("No data available to export.");
    }

    var sb = new StringBuilder();

    // Headers
    var headers = records[0].Keys;
    sb.AppendLine(string.Join(",", headers));

    // Rows
    foreach (var record in records)
    {
      var values = headers.Select(h => $"\"{record[h]?.ToString().Replace("\"", "\"\"")}\"");
      sb.AppendLine(string.Join(",", values));
    }

    if (changeColumnHeader != null)
    {
      changeColumnHeader(sb);
    }

    var bytes = Encoding.UTF8.GetBytes(sb.ToString());

    return new FileContentResult(bytes, "text/csv")
    {
      FileDownloadName = $"{fileName ?? Guid.NewGuid().ToString()}.csv"
    };
  }
}
