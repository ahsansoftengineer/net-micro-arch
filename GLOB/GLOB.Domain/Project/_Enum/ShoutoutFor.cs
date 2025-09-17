using GLOB.Infra.Utils.Attributez;

namespace GLOB.Domain.Enumz;

public enum SHOUTOUT_FOR
{
  [EnumAtr("None")]
  NONE = 0,
  [EnumAtr("For Me")]
  FOR_ME = 1,
  [EnumAtr("For Someone Else")]
  FOR_ELSE = 2,
}
