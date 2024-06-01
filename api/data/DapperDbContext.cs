using System.Data;
using Microsoft.Data.SqlClient;

namespace Gainwell.Data;

public class DapperDbContext {

    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public DapperDbContext(IConfiguration configuration) {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("gainwell-phm");
    }

    public IDbConnection CreateConnection() {
        return new SqlConnection(_connectionString);
    }

}
