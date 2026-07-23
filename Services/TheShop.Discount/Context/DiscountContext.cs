using Microsoft.Data.SqlClient;
using System.Data;

namespace TheShop.Discount.Context
{
    public class DiscountContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DiscountContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
