using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Gainwell.Data;

public class TableValuedParameter(DataTable dataTable, string typeName) : SqlMapper.ICustomQueryParameter {
    
    private readonly DataTable _dataTable = dataTable ?? throw new ArgumentNullException(nameof(dataTable));
    private readonly string _typeName = typeName ?? throw new ArgumentNullException(nameof(typeName));

    public void AddParameter(IDbCommand command, string name) {
        if (command is not SqlCommand sqlCommand) throw new ArgumentException("This parameter can only be added to a SqlCommand", nameof(command));

        var sqlParameter = sqlCommand.Parameters.AddWithValue(name, _dataTable);
        sqlParameter.SqlDbType = SqlDbType.Structured;
        sqlParameter.TypeName = _typeName;
    }
}