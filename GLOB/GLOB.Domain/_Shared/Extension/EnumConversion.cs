// using GLOB.Infra.Utils.Attributez;
// using GLOB.Domain.Enumz;

// namespace GLOB.Domain.Extension;

// public static partial class Extension
// {
//   private static EnumAtrAttribute ToEnumAttr(object? value)
//   {
//     var field = value.GetType().GetField(value.ToString()!);
//     return (EnumAtrAttribute)Attribute.GetCustomAttribute(field, typeof(EnumAtrAttribute));
//   }
//   public static string ToEnumStr1(this string? value)
//   {
//     EnumAtrAttribute attr = ToEnumAttr(value);
//     return attr == null ? "-" : attr.Value1;
//   }
//   public static string ToEnumStr2(this string? value)
//   {
//     EnumAtrAttribute attr = ToEnumAttr(value);
//     return attr == null ? "-" : attr.Value2;
//   }

//   public static string ToGender(this GENDER? value)
//   {
//     EnumAtrAttribute attr = ToEnumAttr(value);
//     return attr == null ? "-" : attr.Value1;
//   }
// }
