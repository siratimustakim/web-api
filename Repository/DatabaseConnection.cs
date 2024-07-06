using Dapper;
using System.Data.SqlClient;

namespace QuranApp.Repository
{
    public interface IDatabaseConnection
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
    }

    public class DatabaseConnection(IConfiguration configuration) : IDatabaseConnection
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<T>(sql, parameters);
        }
    }
}
