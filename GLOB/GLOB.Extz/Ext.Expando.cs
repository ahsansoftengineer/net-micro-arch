using System.Collections.Generic;
using System.Dynamic;

namespace GLOB.Extz;

public static partial class Exts
{
  public static ExpandoObject ToExpando(this object obj)
  {
    if (obj == null)
      return null;

    IDictionary<string, object> expando = new ExpandoObject();

    var properties = obj.GetType().GetProperties();
    foreach (var prop in properties)
      expando[prop.Name] = prop.GetValue(obj);

    return (ExpandoObject)expando;
  }

  public static ExpandoObject ToExpando(this object obj, IDictionary<string, object> extraProps)
  {
    var expando = obj.ToExpando();

    if (extraProps != null)
    {
      var dict = (IDictionary<string, object>)expando;
      foreach (var kv in extraProps)
        dict[kv.Key] = kv.Value;
    }

    return expando;
  }
}