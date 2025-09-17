

namespace SBA.Projectz.Data;
public partial class UOW_Projectz : UOW_Infra, IUOW_Projectz
{
  public UOW_Projectz(DBCtxProjectz context): base(context) { }
}