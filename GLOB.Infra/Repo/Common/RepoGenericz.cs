using GLOB.Domain.Base;
using GLOB.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GLOB.Infra.Repo;

public class RepoGenericz<T> : RepoGenericz<T, int>, IRepoGenericz<T>
  where T : class, IEntityAlpha<int>, IEntityBeta, IEntityStatus
{
  public RepoGenericz(DbContext context) : base(context)
  {
  }
}

public partial class RepoGenericz<T, TKey> : IRepoGenericz<T, TKey>
  where T : class, IEntityAlpha<TKey>, IEntityBeta, IEntityStatus
{
  private readonly DbContext _context;
  private readonly DbSet<T> _db;
  public RepoGenericz(DbContext context)
  {
    _context = context;
    _db = context.Set<T>();
  }
  public DbSet<T> GetDBSet()
  {
    return _db;
  }
  public bool Any(Expression<Func<T, bool>>? filter = null)
  {
    return _db.Any(filter);
  }
  public bool AnyId(TKey Id)
  {
    return Any(x => AreEqual(x.Id, Id));
  }
  public async Task Delete(TKey id)
  {
    var entity = await _db.FindAsync(id);
    if (entity != null) _db.Remove(entity);
  }

  public void DeleteRange(IEnumerable<T> entities)
  {
    _db.RemoveRange(entities);
  }
  public async Task Insert(T entity)
  {
    await _db.AddAsync(entity);
    //await _context.SaveChangesAsync();
  }

  public async Task InsertRange(IEnumerable<T> entities)
  {
    await _db.AddRangeAsync(entities);
  }

  public void Update(T entity)
  {
    _db.Attach(entity);
    _context.Entry(entity).State = EntityState.Modified;
  }
  public void UpdateStatus(T entity, Status status)
  {
    if (typeof(EntityBase).IsAssignableFrom(typeof(T)))
    {
      if (entity is EntityBase baseEntity)
      {
        baseEntity.Status = status;
        _db.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
      }
    }
  }

  private bool AreEqual(TKey a, TKey b)
  {
    return a.Equals(b); // ✅ Safe and clean
  }

}