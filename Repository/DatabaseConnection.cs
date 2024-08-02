using Dapper;
using System.Data.SqlClient;

namespace QuranApp.Repository
{
    public interface IDatabaseConnection
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
    }

    public class DatabaseConnection(IConfiguration configuration) : IDatabaseConnection
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            try
            {
                var a = await conn.ExecuteAsync(sql, parameters);
                return a;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
