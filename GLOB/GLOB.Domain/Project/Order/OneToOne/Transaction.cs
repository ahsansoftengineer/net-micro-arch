using System.ComponentModel.DataAnnotations.Schema;
using GLOB.Domain.Enumz;

namespace GLOB.Domain.Hierarchy;
public class Transactionz : EntityBeta
{
  public int TransactionSimpaisaID { get; set; }
  // Order Price Info
  public int OrderPrice { get; set; } // Setting from Creator Based on Order Type
  public int? InternationalCharges { get; set; } // Comes from User (Standard)
  public int? DiscountPrice { get; set; } // Standard, Custom Discount (Applicable)
  public int? MiscCharges { get; set; }
  public string? Note { get; set; }
  public string? Desc { get; set; }
  [NotMapped]
  public int TotalPrice
  {
    get
    {
      int finalPrice = OrderPrice;
      if (InternationalCharges != null && InternationalCharges > 0)
      {
        finalPrice += (int)InternationalCharges;
      }
      if (MiscCharges != null && MiscCharges > 0)
      {
        finalPrice += (int)MiscCharges;
      }
      if (DiscountPrice != null && DiscountPrice > 0)
      {
        finalPrice -= (int)DiscountPrice;
      }

      return finalPrice;
    }
  } // = OrderPrice - DiscountPrice (+ International Charges if Applicable)

  // Detail of Payment
  public OPERATOR OperatorID { get; set; }
  public STATUS_PAY? StatusPay { get; set; }
  public int? WalletDeduction { get; set; }
  public int? TransactionzPayment { get; set; }
  [NotMapped]
  public int TotalPayment
  {
    get
    {
      int result = 0;
      if (WalletDeduction > 0)
      {
        result += (int)WalletDeduction;
      }
      if (TransactionzPayment > 0)
      {
        result += (int)TransactionzPayment;
      }
      return result;
    }
  } // WalletDeduction + AmountTransactionz

  // Transactionz Verify
  public string? PaymentScreenShot { get; set; }
  public bool IsStarBazaarTransfer { get; set; } = false;

  public OrderBusiness? OrderBusiness { get; set; }
  // public int? OrderBusinessID { get; set; } // No Need
  public OrderStandard? OrderStandard { get; set; }
  // public int? OrderPersonalID { get; set; }
}
