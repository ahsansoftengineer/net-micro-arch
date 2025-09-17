using System.Linq.Expressions;


namespace GLOB.Infra.Repo;

public interface IRepoGenericz<T> : IRepoGenericz<T, int> 
  where T : class, IEntityAlpha<int>
{

}
public interface IRepoGenericz<T, TKey>
  where T : class, IEntityAlpha<TKey>
{
  DbSet<T> GetDBSet();
  bool Any(Expression<Func<T, bool>>? filter = null);
  bool AnyId(TKey? Id);
  Task<T> Get(Expression<Func<T, bool>> expression, List<string>? Includes = null);
  Task<T> Get(TKey Id, List<string>? Includes = null);

  Task<List<T>> Gets(
    Expression<Func<T, bool>>? expression = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    List<string>? Include = null);
  
  Task<T> Add(T entity);
  Task Delete(TKey Id);
  T Update(T entity);
  void UpdateStatus(T entity, Status status);

  Task AddRange(IEnumerable<T> entities);
  void DeleteRange(IEnumerable<T> entities);

  Task<VMPaginate<T>> GetsPaginate<TDtoSearch>(DtoRequestPage<TDtoSearch?> dto)
    where TDtoSearch : class, IDtoSearch;

  Task<VMPaginate<DtoSelect<TKey>>> GetsPaginateOptions<TDtoSearch>(DtoRequestPageOption<TDtoSearch?> dto)
    where TDtoSearch : class, IDtoSearch;
}