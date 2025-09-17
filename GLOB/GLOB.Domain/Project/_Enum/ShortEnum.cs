using GLOB.Infra.Utils.Attributez;

namespace GLOB.Domain.Enumz;

public enum DonationOption
{
  OptionGSB,
  OptionSelf,
  OptionMarhoom
}
public enum GENDER
{
  [EnumAtr("None")]
  NONE = 0,
  [EnumAtr("Male")]
  MALE = 1,
  [EnumAtr("Female", "Icons")]
  FEMALE = 2,
  [EnumAtr("Other")]
  OTHER = 3,
}
public enum LANGUAGE
{
  [EnumAtr("None")]
  NONE = 0,
  [EnumAtr("Urdu")]
  URDU = 1,
  [EnumAtr("English")]
  ENG = 2,
}
//public class Status
//{
//  private Status(string value) { Value = value; }

//  public string Value { get; private set; }

//  public static Status None { get { return new Status("None"); } }
//  public static Status Activate { get { return new Status("Activate"); } }
//  public static Status DeActivate { get { return new Status("De-Activate"); } }
//  public override string ToString()
//  {
//  return Value;
//  }
//}