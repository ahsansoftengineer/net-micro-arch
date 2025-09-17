namespace GLOB.Infra.Utils.Attributez;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class EnumAtrAttribute : Attribute
{
  public string Value1 { get; }
  public string Value2 { get; }

  public EnumAtrAttribute(string value1, string? value2 = null)
  {
    Value1 = value1;
    Value2 = value2 == null ? "-" : value2;
  }
}
