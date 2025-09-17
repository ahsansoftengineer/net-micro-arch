using System.ComponentModel.DataAnnotations.Schema;
using GLOB.Domain.Enumz;

namespace GLOB.Domain.Hierarchy;

public class OrderStandard : BaseOrder
{
  public int? UserStandardID { get; set; }
  [NotMapped]
  public UserStandard? UserStandard { get; set; }

  public int? TransactionzID { get; set; }
  public Transactionz? Transactionz { get; set; }
  // Detail of Shoutout Order
  public SHOUTOUT_FOR ShoutoutFor { get; set; }
  public LANGUAGE Language { get; set; }
  public string? SenderName { get; set; }
  public GENDER? SenderGender { get; set; }
  public required string ReciverName { get; set; }
  public GENDER ReciverGender { get; set; }
  public required string OrderTitle { get; set; } // Cusotm, Standard Events (Aniversery, Birthday, Eid ...)

  public bool IsVideoPublic { get; set; } = false;
}
// {

//  "shoutout": {
//     "Id": "1655921085356",
//     "message": "Saal Girah Mubarak Salman Sahgal from Tayyab Chuhdary",
//     "title": "Birthday Shoutout",
//     "amount": 5000,
//     "walletDeduction": 0,
//     "discount": 0,
//     "totalAmount": 5000,
//     "amountFromOnlineTransactionz": 5000,
//     "operatorId": "100008",
//     "isInternationalClient": false
//   }
// }
