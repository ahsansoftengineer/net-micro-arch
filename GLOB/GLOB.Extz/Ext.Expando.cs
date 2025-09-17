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

  public static ExpandoObject ToExpando(this object obj, params (string Key, object Value)[] items)
  {
    var expando = obj.ToExpando();

    if (items != null)
    {
      var dict = (IDictionary<string, object>)expando;
      foreach (var kv in items)
        dict[kv.Key] = kv.Value;
    }

    return expando;
  }
}