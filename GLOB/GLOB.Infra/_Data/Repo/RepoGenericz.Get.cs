
using System.Linq.Expressions;

namespace GLOB.Infra.Repo;
public partial class RepoGenericz<T, TKey>
{
  public async Task<T> Get(TKey Id, List<string?>? Include = null)
  {
    return await Get(x => x.Id.ToString() == Id.ToString(), Include);
  }
  public async Task<T> Get(
   Expression<Func<T, bool>> expression,
   List<string?>? Include = null)
  {
    IQueryable<T> query = _db;
    query = query.ToExtQueryInclues(Include);
    return await query.AsNoTracking().FirstOrDefaultAsync(expression);
  }
}