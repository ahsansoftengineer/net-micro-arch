

namespace GLOB.Infra.Extz;

public static partial class ExtResponse
{
  public static async Task<VMPaginate<DtoSelect<int>>> ToExtVMPageOptionsNoTrack<T>(
      this IQueryable<T> query,
      DtoRequestPageOption<DtoSearch?> dto)
    where T : class, IEntityAlpha, IEntityBeta, IEntityStatus
  {
      return await query.ToExtVMPageOptionsNoTrack<T, int>(dto);
  }
  public static async Task<VMPaginate<DtoSelect<TKey>>> ToExtVMPageOptionsNoTrack<T, TKey>(
      this IQueryable<T> query,
      DtoRequestPageOption<DtoSearch?> req)
    where T : class, IEntityAlpha<TKey>, IEntityBeta, IEntityStatus
  {
    return await query.ToExtVMPageOptionsNoTrack<T, TKey, DtoSearch>(req);
  }

  public static async Task<VMPaginate<DtoSelect<TKey>>> ToExtVMPageOptionsNoTrack<T, TKey, TDtoSearch>(
      this IQueryable<T> query,
      DtoRequestPageOption<TDtoSearch?> req)
    where TDtoSearch : class, IDtoSearch
    where T : class, IEntityAlpha<TKey>, IEntityBeta, IEntityStatus
  {
    query = query.ToExtQueryFilter(req.Filter); // Fix the Adding Enums to Every Filter
    query = query.ToExtQueryOrderBy(req.Sort);
 
    var result =  query.ToExtMapSelect<T, TKey>(); // IEntityAlpha, IEntityStatus

    return await result.AsNoTracking().ToExtPageReq(req);
  }

}
