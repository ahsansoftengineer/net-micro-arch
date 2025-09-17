using GLOB.Infra.Repo;

namespace GLOB.API.Staticz;

public static partial class _Actionz
{
  public async static Task<IActionResult> ToActionGetsPaginate<TEntity, TDtoSearch>(this IRepoGenericz<TEntity> repo, DtoRequestPage<TDtoSearch?> dto)
    where TEntity : class, IEntityAlpha
    where TDtoSearch : class, IDtoSearch
  {
    var data = await repo.GetsPaginate(dto);
    return data.Ok();
  }

  public async static Task<IActionResult> ToActionGetsPaginateOptions<TEntity, TDtoSearch>(this IRepoGenericz<TEntity> repo, DtoRequestPageOption<TDtoSearch?> dto)
    where TEntity : class, IEntityAlpha
    where TDtoSearch : class, IDtoSearch
  {
    var data = await repo.GetsPaginateOptions(dto);
    return data.Ok();
  }
}