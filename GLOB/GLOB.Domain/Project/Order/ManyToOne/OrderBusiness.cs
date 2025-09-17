
using System.ComponentModel.DataAnnotations.Schema;

namespace GLOB.Domain.Hierarchy;
public class OrderBusiness : BaseOrder
{
  // User Values
  public required string Phone { get; set; }
  public required string Email { get; set; }
  public required string UrlBusinessWebsite { get; set; }
  public required string ContactPerson { get; set; }
  public bool AgreeToTermsCondition { get; set; } = false;
  public bool AgreeToPrivacyPolicy { get; set; } = false;

  [NotMapped]
  public string? Password { get; set; } = "";
  [NotMapped]
  public string? ConfirmPassword { get; set; } = "";

  // -----  
  // Business Inquiry
  // Base.Message = Inquiry Details
  public string? InquiryBudget { get; set; } // (0 - 50,000 PKR)  

  // public object BusinessMarketingRights { get; set; } = { days_15 = 1000, days_30 = 2000}

  public string? Script { get; set; }
  public string? AdminComment { get; set; }
  // *-.
  public int? UserBusinessID { get; set; }
  [NotMapped]
  public UserBusiness? UserBusiness { get; set; }
  public int? OrderBusinessMultiID { get; set; }
  public OrderBusinessMulti? OrderBusinessMulti { get; set; }

  public int? OrderAddOnID { get; set; }
  public OrderAddOn? OrderAddOn { get; set; }

  public Transactionz? Transactionz { get; set; }
  public int? TransactionzID { get; set; }
}
