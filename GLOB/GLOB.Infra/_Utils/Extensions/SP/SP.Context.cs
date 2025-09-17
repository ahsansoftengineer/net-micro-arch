using Microsoft.Data.SqlClient;


namespace GLOB.Infra.Extz;

public static class DBCtxExtensions
{
    // For returning entities (SELECT)
    public static List<T> ExecSP<T>(this DBCtxInfra context, string procedureName, params SqlParameter[] parameters) where T : class
    {
        var sql = BuildSqlWithParams(procedureName, parameters.Length);
        return context.Set<T>().FromSqlRaw(sql, parameters).ToList();
    }

    // For non-queries (e.g., INSERT/UPDATE/DELETE)
    public static int ExecSPNonQuery(this DBCtxInfra context, string procedureName, params SqlParameter[] parameters)
    {
        var sql = BuildSqlWithParams(procedureName, parameters.Length);
        return context.Database.ExecuteSqlRaw(sql, parameters);
    }

    private static string BuildSqlWithParams(string procName, int paramCount)
    {
        if (paramCount == 0)
            return $"EXEC {procName}";

        var paramPlaceholders = string.Join(", ", Enumerable.Range(0, paramCount).Select(i => $"{{{i}}}"));
        return $"EXEC {procName} {paramPlaceholders}";
    }
}
// var param = new SqlParameter("@RoleName", "Admin");

// var users = DBCtxInfra.ExecuteStoredProcedure<User>("GetUsersByRole", param);

// var idParam = new SqlParameter("@Id", 1);
// var nameParam = new SqlParameter("@Name", "UpdatedName");

// var rowsAffected = DBCtxInfra.ExecuteStoredProcedureNonQuery("UpdateUserName", idParam, nameParam);

// var users = DBCtxInfra.Set<UserSummary>()
//     .FromSqlRaw("EXEC GetUserSummaries")
//     .AsNoTracking()
//     .ToList();
// protected override void Up(MigrationBuilder migrationBuilder)
// {
//     var spSql = File.ReadAllText("Migrations/SQL/GetUsersByRole.sql");
//     migrationBuilder.Sql(spSql);
// }