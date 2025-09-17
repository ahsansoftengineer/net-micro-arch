
using GLOB.Infra.Repo;


namespace GLOB.API.Staticz;

public static partial class _Actionz
{
  public static async Task<IActionResult> ToActionGets<T>(this IRepoGenericz<T> repo, List<string>? Include)
    where T : class, IEntityAlpha
  {
      var list = await repo.Gets(Include: Include);
      return list.ToExtVMList().Ok();
  }
  public static async Task<IActionResult> ToActionGetsLookup<T>(this IRepoGenericz<T> repo)
    where T : class, IEntityAlpha
  {
      var list = await repo.GetDBSet()
        .Select(x => new {x.Id, x.Name})
        .ToDictionaryAsync(x => x.Id, y => y.Name);
      return list.ToExtVMSingle().Ok();
  }
}