using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace GLOB.API.Config.OptionSetup;
public class SwaggerNullableEnumSchemaFilter : ISchemaFilter
{
  public void Apply(OpenApiSchema schema, SchemaFilterContext context)
  {
    var type = context.Type;

    if (type.IsGenericType &&
      type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
      type.GetGenericArguments()[0].IsEnum)
    {
      var enumType = type.GetGenericArguments()[0];
      schema.Type = "string";
      schema.Nullable = true;
      schema.Enum = Enum.GetNames(enumType)
        .Select(name => new Microsoft.OpenApi.Any.OpenApiString(name))
        .Cast<Microsoft.OpenApi.Any.IOpenApiAny>()
        .ToList();
    }
  }
}
