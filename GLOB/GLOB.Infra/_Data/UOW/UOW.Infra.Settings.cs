

namespace GLOB.Infra.UOW;
public partial class UOW_Infra 
{
  public readonly DbContext _context;

  public UOW_Infra(DbContext context)
  {
    _context = context;
  }

  public async Task Save()
  {
    AddTimestamps();
    await _context.SaveChangesAsync();
  }
  // private IRepoGenericz<T> Got<T>() where T : EntityBase
  // {
  //   return new RepoGenericz<T>(_context);
  // }

  // TODO: ARCH:  This AddTimeStamp is not Working for Seed
  private void AddTimestamps()
  {
    var entities = _context.ChangeTracker.Entries()
      .Where(x => x.Entity is IEntityBeta && (x.State == EntityState.Added || x.State == EntityState.Modified));

    foreach (var entity in entities)
    {
      var now = DateTimeOffset.UtcNow; // current datetime
      entity.Print();
      if (entity.State == EntityState.Added)
      {
        ((IEntityBeta)entity.Entity).CreatedAt = now;
      }
    //EntityState.Detached, EntityState.Deleted, EntityState.Unchanged
    ((IEntityBeta)entity.Entity).UpdatedAt = now;
    }
  }
  public void Dispose()
  {
    _context.Dispose();
    GC.SuppressFinalize(this);
  }
}