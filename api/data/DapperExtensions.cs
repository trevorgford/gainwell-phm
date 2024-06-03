using System.Data;
using Dapper;

namespace Gainwell.Data;

public static class DapperExtensions {

    public static IEnumerable<T> QueryStoredProcedure<T>(this IDbConnection db, string storedProcedure, object? parameters = null) {
        return db.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public static async Task<IEnumerable<T>> QueryStoredProcedureAsync<T>(this IDbConnection db, string storedProcedure, object? parameters = null) {
        return await db.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    // public static async Task<IEnumerable<DataTable>> QueryMultipleAsync3<DataTable>(this IDbConnection db, string storedProcedure, object? parameters = null) {
    //     using var results = await db.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    //     return await results.ReadAsync<DataTable>();
    //     // return new {
    //     //     Results = await results.ReadAsync(),
    //     //     Total = await results.ReadSingleAsync<int>()
    //     // };
    // }

    public static async Task<List<DataTable>> ExecuteDataTablesAsync(this IDbConnection db, string storedProcedure, object? parameters = null) {
        using var reader = await db.ExecuteReaderAsync(storedProcedure, parameters);

        var result = new List<DataTable>();
        do {
            var dataTable = new DataTable();
            dataTable.Load(reader);
            result.Add(dataTable);
        } while (!reader.IsClosed);

        return result;
    }

    public static int ExecuteStoredProcedure(this IDbConnection db, string storedProcedure, object? parameters = null) {
        return db.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public static async Task<int> ExecuteStoredProcedureAsync(this IDbConnection db, string storedProcedure, DynamicParameters? parameters = null) {
        return await db.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public static async Task<string> ExecuteScalarStringAsync(this IDbConnection db, string storedProcedure, object? parameters = null) {
        string? result = await db.ExecuteScalarAsync<string>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        return result ?? string.Empty;
    }

    public static async Task<int?> ExecuteScalarIntAsync(this IDbConnection db, string storedProcedure, object? parameters = null) {
        return await db.ExecuteScalarAsync<int?>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public static SqlMapper.ICustomQueryParameter AsTableParameter(this DataTable dataTable, string typeName) {
        return new TableValuedParameter(dataTable, typeName);
    }

}
