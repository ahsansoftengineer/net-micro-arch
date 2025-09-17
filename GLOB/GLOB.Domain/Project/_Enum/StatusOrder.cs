
using GLOB.Infra.Utils.Attributez;

namespace GLOB.Domain.Enumz;
public enum STATUS_ORDER
{
  [EnumAtr("None")]
  NONE = 0,
  // CUSTOMER
  [EnumAtr("New Order", "text-info")]
  NEW_ORDER = 1,
  [EnumAtr("Unpaid", "text-danger")]
  UNPAID = 2,
  [EnumAtr("Paid", "text-warning")]
  PAID = 3,

  // CREATOR
  [EnumAtr("Pending", "text-primary")]
  PENDING = 4,
  [EnumAtr("Accepted", "text-success")]
  ACCEPTED = 5,
  [EnumAtr("Cancelled", "text-danger")]
  CANCELLED = 6,
  [EnumAtr("Completed", "text-success")]
  COMPLETED = 7,
  [EnumAtr("Declined", "text-danger")]
  DECLINED = 8,
  [EnumAtr("Expired", "text-secondary")]
  EXPIRED = 9,
}
