using MySqlConnector;
using Maintenance___Work_Orders_API.Infrastructure.DB;
using System.Data;

namespace Maintenance___Work_Orders_API.Infrastructure
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        private ILogger<MySqlConnectionFactory> _logger;
        public MySqlConnectionFactory(IConfiguration configuration, ILogger<MySqlConnectionFactory> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("MySql") ?? throw new InvalidOperationException("Database connection string is not configured");
            if (string.IsNullOrEmpty(_connectionString))
            {
                _logger.LogError("Database connection string is null or empty");
                throw new InvalidOperationException("Database connection string is not configured properly");
            }
        }
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
