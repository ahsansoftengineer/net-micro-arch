using GLOB.Infra.Repo;
using GLOB.Domain.Hierarchy;
using GLOB.Hierarchy.Global;
namespace SBA.Projectz.Data;
public interface IUOW_Projectz : IUOW_Infra
{
  // .-*
  IRepoGenericz<GlobalLookupBase> GlobalLookupBases { get; }
  IRepoGenericz<Org> Orgs { get; }
  IRepoGenericz<BG> BGs { get; }
  IRepoGenericz<State> States { get; }
  IRepoGenericz<Bank> Banks { get; }
  IRepoGenericz<Brand> Brands { get; }
  IRepoGenericz<Industry> Industrys { get; }
  IRepoGenericz<Profession> Professions { get; }
  // *-.
  IRepoGenericz<GlobalLookup> GlobalLookups { get; }
  IRepoGenericz<Systemz> Systemzs { get; }
  IRepoGenericz<LE> LEs { get; }
  IRepoGenericz<OU> OUs { get; }
  IRepoGenericz<SU> SUs { get; }
  IRepoGenericz<City> Citys { get; }
}