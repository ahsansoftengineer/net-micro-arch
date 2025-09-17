using System.Reflection;

namespace GLOB.Infra.Extz;
public static partial class ExtQuery
{
  public static IQueryable<T> ToExtQueryInclues<T>(this IQueryable<T> source, List<string?>? Include)
    where T : class
  {
    if (Include == null || Include?.Count < 1) return source;

    Type entityType = typeof(T);
    IEnumerable<PropertyInfo> navigationProperty = entityType.GetProperties()
      .Where(property =>
      Attribute.IsDefined(property, typeof(RemoteAttribute)) ||
      property.GetAccessors().Any(a => a.IsVirtual)
      );
    IEqualityComparer<string?> comparer = new CustomStringEqualityComparer();
    var matchingProperties = navigationProperty.Select(x => x.Name).Intersect(Include, comparer);

    if (matchingProperties != null)
    {
      foreach (var item in matchingProperties)
      {
        source = source.Include(item);
      }
    }
    return source;
  }
}
public class CustomStringComparer : IComparer<string>
{
  public int Compare(string x, string y)
  {
    // Customize the comparison logic here
    return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
  }
}

public class CustomStringEqualityComparer : IEqualityComparer<string>
{
  public bool Equals(string x, string y)
  {
    // Customize the string comparison logic here
    return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
  }

  public int GetHashCode(string obj)
  {
    // Customize the hash code generation logic here
    return StringComparer.OrdinalIgnoreCase.GetHashCode(obj);
  }
}