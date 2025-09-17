using GLOB.Infra.Repo;
using GLOB.Hierarchy.Global;
namespace SBA.Projectz.Data;
public partial class UOW_Projectz : UOW_Infra, IUOW_Projectz
{
  public UOW_Projectz(DBCtxProjectz context): base(context) { }
  // .-*
  private IRepoGenericz<GlobalLookupBase>? _GlobalLookupBase;
  
  // *-.
  private IRepoGenericz<GlobalLookup>? _GlobalLookup;
}