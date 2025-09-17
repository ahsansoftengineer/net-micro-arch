using Microsoft.Data.SqlClient;

namespace GLOB.Infra.Extz;

public static partial class Exts_Infra
{
  public static bool EnsureDatabaseExists(this string con)
  {
    "Ensuring DB Exist".Print("EF Core");
    con.Print("EF Core");

    var builder = new SqlConnectionStringBuilder(con);
    string db = builder.InitialCatalog;

    // Use 'master' DB to perform existence check/creation
    builder.InitialCatalog = "master";

    using var connection = new SqlConnection(builder.ConnectionString);
    connection.Open();

    // Check if database exists
    var checkCmdText = $"SELECT COUNT(*) FROM sys.databases WHERE name = @dbName";
    using var checkCmd = new SqlCommand(checkCmdText, connection);
    checkCmd.Parameters.AddWithValue("@dbName", db);

    var exists = (int)checkCmd.ExecuteScalar() > 0;

    if (!exists)
    {
      "DB is not Exsist".Print("EF Core");
      var createCmdText = $"CREATE DATABASE [{db}]";
      using var createCmd = new SqlCommand(createCmdText, connection);
      createCmd.ExecuteNonQuery();
    }
    return exists;
  }
}