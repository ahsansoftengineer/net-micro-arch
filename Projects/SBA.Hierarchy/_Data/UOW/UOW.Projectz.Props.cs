using GLOB.Infra.Repo;
using GLOB.Domain.Hierarchy;
using GLOB.Hierarchy.Global;
namespace SBA.Projectz.Data;
public partial class UOW_Projectz : UOW_Infra, IUOW_Projectz
{
  public UOW_Projectz(DBCtxProjectz context): base(context) { }
  // .-*
  private IRepoGenericz<GlobalLookupBase>? _GlobalLookupBase;
  private IRepoGenericz<Org>? _Orgs;
  private IRepoGenericz<BG>? _BG;
  private IRepoGenericz<State>? _State;
  private IRepoGenericz<Bank>? _Bank;
  private IRepoGenericz<Brand>? _Brand;
  private IRepoGenericz<Industry>? _Industry;
  private IRepoGenericz<Profession>? _Profession;
  
  // *-.
  private IRepoGenericz<GlobalLookup>? _GlobalLookup;
  private IRepoGenericz<Systemz>? _Systemz;
  private IRepoGenericz<LE>? _LE;
  private IRepoGenericz<OU>? _OU;
  private IRepoGenericz<SU>? _SU;
  private IRepoGenericz<City>? _City;

    
}