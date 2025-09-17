namespace GLOB.Infra.UOW;
public interface IUOW_Infra : IDisposable
{
  Task Save();
  IRepoGenericz<ProjectzLookupBase> ProjectzLookupBases { get; }
  IRepoGenericz<ProjectzLookup> ProjectzLookups { get; }
}