using System.Data;

namespace Maintenance___Work_Orders_API.Infrastructure.DB
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
