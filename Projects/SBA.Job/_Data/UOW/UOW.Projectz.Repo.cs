using GLOB.Infra.Repo;
using GLOB.Hierarchy.Global;

namespace SBA.Projectz.Data;
public partial class UOW_Projectz
{
  // .-*
  public IRepoGenericz<GlobalLookupBase> GlobalLookupBases => _GlobalLookupBase ??= new RepoGenericz<GlobalLookupBase>(_context);

  // *-.
  public IRepoGenericz<GlobalLookup> GlobalLookups => _GlobalLookup ??= new RepoGenericz<GlobalLookup>(_context);
}