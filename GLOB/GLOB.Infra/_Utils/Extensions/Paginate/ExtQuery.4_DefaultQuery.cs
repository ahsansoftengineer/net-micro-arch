using System.Linq.Expressions;

namespace GLOB.Infra.Extz;

public static partial class ExtQuery
{
  public static IQueryable<T> ToExtQuery_Query<T>(
      this IQueryable<T> query,
      Expression<Func<T, bool>>? expression,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
      List<string?>? Include)
    where T : class
  {
    if (expression != null)
    {
      query = query.Where(expression);
    }

    query = query.ToExtQueryInclues(Include);

    if (orderBy != null)
    {
      query = orderBy(query);
    }
    return query;
  }
}
