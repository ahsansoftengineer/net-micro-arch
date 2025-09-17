using GLOB.Infra.Utils.Attributez;

namespace GLOB.Domain.Enumz;

public enum OPERATOR
{
  [EnumAtr("None")]
  NONE = 0,
  [EnumAtr("Screen Shot")]
  SCREEN_SHOT = 1,
  [EnumAtr("Easy Paisa", "Icons")]
  EASY_PAISA = 100007,
  [EnumAtr("Jazz Cash")]
  JAZZ_CASH = 100008,
  [EnumAtr("HBL Konnect")]
  HBL_KONNECT = 100014,
  [EnumAtr("Alfa X")]
  ALFA_X = 100012,
  [EnumAtr("Bank Transfer")]
  BANK_TRANSFER = 100018,
  [EnumAtr("Link 1")]
  LINK_1 = 100011,
  [EnumAtr("Visa Master")]
  VISA_MASTER = 100013,
}
