using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GLOB.API.Config.OptionSetup;
public class SwaggerNullablePrimitivesSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null) return;

        foreach (var prop in schema.Properties)
        {
            var propSchema = prop.Value;

            if (propSchema.Nullable 
                && propSchema.Default == null 
                && IsSimpleOrDate(propSchema.Type, propSchema.Format))
            {
                propSchema.Default = new OpenApiNull();
            }
        }
    }

    private bool IsSimpleOrDate(string type, string format)
    {
        return type switch
        {
            "string" when format == "date" || format == "date-time" => true,
            "string" => true,
            "number" => true,
            "integer" => true,
            "boolean" => true,
            _ => false
        };
    }
}
