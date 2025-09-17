

namespace GLOB.Infra.Extz;
public static partial class ExtQuery
{
  public static IQueryable<DtoSelect<TKey>> ToExtMapSelect<T, TKey>(this IQueryable<T> source)
    where T : class, IEntityAlpha<TKey>, IEntityStatus
  {
    return source.Select(x => new DtoSelect<TKey>
    {
      Id = x.Id,
      Name = x.Name,
      Status = x.Status
    });
  }

  public static IQueryable<DtoSelect<int>> ToExtMapSelect<T>(this IQueryable<T> source)
    where T : class, IEntityAlpha<int>, IEntityStatus
  {
    return source.ToExtMapSelect<T, int>();
  }
}
