using GLOB.Infra.Utils.Attributez;

namespace GLOB.Domain.Enumz;
public enum STATUS_PAY
{
  [EnumAtr("None")]
  NONE = 0,
  [EnumAtr("Unpaid", "text-danger")]
  UNPAID = 1,
  [EnumAtr("Pending", "text-primary")]
  PENDING = 2,
  [EnumAtr("Paid", "text-warning")]
  PAID = 3,
  [EnumAtr("Expired", "text-secondary")]
  EXPIRED = 4,
  // Simpaisa
  [EnumAtr("Review", "text-primary")]
  REVIEW = 5,
  [EnumAtr("Publishing", "text-info")]
  PUBLISHING = 6,
  [EnumAtr("Published", "text-success")]
  PUBLISHED = 7,
  [EnumAtr("Disbursed", "text-info")]
  DISBURSED = 8,
  [EnumAtr("Rejected", "text-danger")]
  REJECTED = 9,
  [EnumAtr("Hold", "text-info")]
  HOLD = 10,
  [EnumAtr("Refund", "text-danger")]
  REFUND = 11,
}
