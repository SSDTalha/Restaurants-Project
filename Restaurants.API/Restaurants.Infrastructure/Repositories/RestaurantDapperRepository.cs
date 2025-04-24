using Dapper;
using Microsoft.Data.SqlClient;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;


namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantDapperRepository(SqlConnection connection) : IDapperRepository, IGenericRepository<Restaurant>
    {
        private readonly SqlConnection _connection = connection;

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            // Ensure the connection is opened before querying
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            return await _connection.QueryAsync<Restaurant>("SELECT * FROM Restaurants");
        }

        public async Task<IEnumerable<Restaurant>> QueryAsync(ISpecification<Restaurant> spec)
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            string whereClause = spec.ToWhereClause();
            string sql = $"SELECT * FROM Restaurants {(string.IsNullOrEmpty(whereClause) ? "" : $"WHERE {whereClause}")}";

            return await _connection.QueryAsync<Restaurant>(sql, spec.GetParameters());
        }
    }
}