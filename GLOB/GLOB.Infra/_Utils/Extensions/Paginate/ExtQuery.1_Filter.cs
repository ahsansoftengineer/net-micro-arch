using LinqKit;

using System.Linq.Expressions;
using System.Reflection;

namespace GLOB.Infra.Extz;
public static partial class ExtQuery
{
  public static IQueryable<T> ToExtQueryFilter<T, TDtoSearch>(this IQueryable<T> source, TDtoSearch? DtoSearch)
    where T : class, IEntityBeta
    where TDtoSearch : class, IDtoSearch
  {
    if (DtoSearch == null) return source;
    if(DtoSearch.DateFrom != null) 
      source = source.Where(x => x.UpdatedAt >= DtoSearch.DateFrom.Value);
    if(DtoSearch.DateTo != null)
      source = source.Where(x => x.UpdatedAt <= DtoSearch.DateTo.Value);

    //var dtoParam = Expression.Parameter(typeof(TDtoSearch), "TDtoSearch"); //
    var entityParam = Expression.Parameter(typeof(T), "TEntity");
    var dtoPropInfos = DtoSearch.GetType()
      .GetProperties(BindingFlags.Public | BindingFlags.Instance)
      .Where(p => p.GetValue(DtoSearch) != null); // && p.PropertyType.IsValueType

    var predicate = PredicateBuilder.New<T>(true);

    foreach (var dtoPropInfo in dtoPropInfos)
    {
      if (!dtoPropInfo.Name.HasValidProperty<T>())
      {
        continue;
      }
      MemberExpression? entityProp = null;
      try
      {
        entityProp = Expression.Property(entityParam, dtoPropInfo.Name);
      }
      catch (Exception e)
      {
        $"Property not Exist {dtoPropInfo.Name} Message : {e.Message}".Print("Exception");
      }
      if (entityProp != null)
      {
        var entityValue = dtoPropInfo.GetValue(DtoSearch);
        var constant = Expression.Constant(entityValue, entityProp.Type);

        if (!string.IsNullOrWhiteSpace(entityValue?.ToString()))
        {
          if (entityValue.GetType() == typeof(string))
          {
            predicate = predicate.And(p => EF.Functions.Like(
            EF.Property<string>(p, dtoPropInfo.Name),
            $"%{entityValue}%")
             );
          }
          else
          {
            var comparison = Expression.Equal(entityProp, constant);
            var lambda = Expression.Lambda(comparison, entityParam);
            predicate = predicate.And((Expression<Func<T, bool>>)lambda);
          }

        }
      }
    }
    return source.Where(predicate);
  }
}