using Dapper;
using System.Data.SqlClient;

namespace QuranApp.Repository
{
    public interface IDatabaseConnection
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
        Task<T> QuerySingleAsync<T>(string sql, object parameters = null);
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
                return await conn.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object parameters = null)
        {
            using var conn = new SqlConnection(_connectionString);
            try
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
