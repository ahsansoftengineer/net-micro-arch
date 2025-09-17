using System.Linq.Dynamic.Core;

namespace GLOB.Infra.Extz;
public static partial class ExtQuery
{
  public static IQueryable<T> ToExtQueryOrderBy<T>(this IQueryable<T> query, Sort? sort)
    where T : class, IEntityBeta
  {
    if (sort == null || string.IsNullOrEmpty(sort?.By))
    {
      sort = new Sort()
      {
        By = "none",
        Order = Order.Ascending
      };
    }

    string sortOrder = sort?.Order == Order.Descending ? "desc" : "asc";
    try
    {
      if (!sort.By.HasValidProperty<T>())
      {
        sortOrder = "desc";
        return query.OrderBy($"UpdatedAt {sortOrder}");
      }
      return query.OrderBy($"{sort.By} {sortOrder}");
    }
    catch (Exception ex)
    {
      $"Property not exist {sort.By} {ex.Message} on Type {nameof(T)}".Print("Exception");
      return query;
    }
  }

}