namespace GLOB.Extz;
public static partial class Exts
{
  public static bool HasValidProperty<T>(this string propertyName)
  {
    var property = typeof(T).GetProperty(propertyName);

    if (property == null)
      return false;

    Type propertyType = property.PropertyType;

    // Check if the type is a primitive, value type (like DateTime, Guid), or a common DB column type
    bool isValidType = propertyType.IsPrimitive
      || propertyType.IsValueType
      || propertyType == typeof(string)
      || propertyType == typeof(decimal);

    // Exclude complex classes except nullable types
    if (propertyType.IsClass && propertyType != typeof(string))
    {
      var underlyingType = Nullable.GetUnderlyingType(propertyType);
      if (underlyingType == null)
        return false;

      // Check if the underlying type is a valid type (e.g., int?, DateTime?)
      isValidType = underlyingType.IsPrimitive
        || underlyingType.IsValueType
        || underlyingType == typeof(decimal);
    }

    return isValidType;
  }
}