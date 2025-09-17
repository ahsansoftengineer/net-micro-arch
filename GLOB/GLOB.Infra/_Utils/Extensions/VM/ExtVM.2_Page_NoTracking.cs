using System.Linq.Expressions;


namespace GLOB.Infra.Extz;

public static partial class ExtResponse
{
  public static async Task<List<T>> ToExtList<T>(
    this IQueryable<T> query,
    Expression<Func<T, bool>>? expression,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
    List<string?>? Includes)
    where T : class
  {
    return await query.ToExtQuery_Query(expression, orderBy, Includes).AsNoTracking().ToListAsync();
  }
  public static async Task<VMPaginate<T>> ToExtPageReq<T, TDtoSearch>(
    this IQueryable<T> source, DtoRequestPageOption<TDtoSearch?> dto)
  {
    var dtoPage = new DtoPage(){
      PageNo = dto.PageNo,
      PageSize = dto.PageSize,
    };
    return await source.ToExtVMPage(new(dtoPage));
  }

  public static async Task<VMPaginate<T>> ToExtVMPageNoTrack<T, TDtoSearch>(
      this IQueryable<T> query,
      DtoRequestPage<TDtoSearch?> dto)
    where TDtoSearch : class, IDtoSearch
    where T : class, IEntityBeta
  {
    query = query.ToExtQueryFilter(dto.Filter); // Fix the Adding Enums to Every Filter
    query = query.ToExtQueryOrderBy(dto.Sort); // IEntityBeta
    query = query.ToExtQueryInclues(dto?.Includes);

    return await query.AsNoTracking().ToExtPageReq(dto);
  }
 

}
