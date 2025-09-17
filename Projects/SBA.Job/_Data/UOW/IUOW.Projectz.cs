using GLOB.Infra.Repo;
using GLOB.Hierarchy.Global;
namespace SBA.Projectz.Data;
public interface IUOW_Projectz : IUOW_Infra
{
  // .-*
  IRepoGenericz<GlobalLookupBase> GlobalLookupBases { get; }

  // *-.
  IRepoGenericz<GlobalLookup> GlobalLookups { get; }
}