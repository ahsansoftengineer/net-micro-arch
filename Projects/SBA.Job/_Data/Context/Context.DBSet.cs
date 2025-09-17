using GLOB.Hierarchy.Global;


namespace SBA.Projectz.Data;

public partial class DBCtxProjectz
{
  // .-*
  public DbSet<GlobalLookupBase> GlobalLookupBases { get; set; }
  public DbSet<GlobalLookup> GlobalLookups { get; set; }
  public DbSet<NewTable> NewTables { get; set; }
  public DbSet<NewTable2> NewTable2s { get; set; }
  public DbSet<NewTable3> NewTable3s { get; set; }
}

public class NewTable : EntityBase
{

}
public class NewTable2 : EntityBase
{

}
public class NewTable3 : EntityBase
{

}