using Dapper;
using Microsoft.Data.SqlClient;

namespace AquariumAutomationAPI.Context
{
    public class DataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
        public IEnumerable<T> LoadData<T>(string sql, object? param)
        {
            var _connection = CreateConnection();
            return _connection.Query<T>(sql, param);
        }
        public IEnumerable<T> ExecuteStoredProcedure<T>(string sql, object? param)
        {
            var _connection = CreateConnection();
            return _connection.Query<T>(sql, param, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
