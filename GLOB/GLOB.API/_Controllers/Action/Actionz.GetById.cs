using GLOB.Infra.Repo;


namespace GLOB.API.Staticz;

public static partial class _Actionz
{
 
  public static async Task<IActionResult> ToActionGet<T>(this IRepoGenericz<T> repo, int Id, List<string>? Include)
    where T : class, IEntityAlpha
  {
      var single = await repo.Get(Id, Include);
      if(single == null) return _Res.NotFoundId(Id);
      return single.ToExtVMSingle().Ok();
  }
  public static async Task<IActionResult> ToActionGetsByIds<T>(this IRepoGenericz<T> repo, List<int>? Ids)
    where T : class, IEntityAlpha
  {
      var list = await repo.Gets((x)=> Ids.Contains(x.Id));
      return list.ToExtVMList().Ok();
  }
  public static async Task<IActionResult> ToActionGetsByIdsLookup<T>(this IRepoGenericz<T> repo, List<int>? Ids)
    where T : class, IEntityAlpha
  {
      var list = await repo.GetDBSet()
        .Select(x => new { x.Id, x.Name })
        .Where((x)=> Ids.Contains(x.Id))
        .ToDictionaryAsync(x => x.Id, y => y.Name);
      return list.ToExtVMSingle().Ok();
  }
}