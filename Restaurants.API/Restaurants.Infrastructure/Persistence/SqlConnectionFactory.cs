using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Persistence;

public class SqlConnectionFactory(RestaurantsDbContext context)
{
    private readonly RestaurantsDbContext _context = context;

    public SqlConnection CreateConnection()
    {
        return (SqlConnection)_context.Database.GetDbConnection();
    }
}