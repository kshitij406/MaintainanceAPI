using Dapper;
using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Infrastructure.DB;

namespace Maintenance___Work_Orders_API.Infrastructure.Repository
{
    public class SqlWorkOrderRepo : ISqlWorkOrderRepo
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SqlWorkOrderRepo(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<WorkOrder> GetAllWorkOrders()
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            const string sql = @"
                SELECT 
                    id AS Id,
                    asset_id AS AssetId,
                    title AS Title,
                    description AS Description,
                    priority AS Priority,
                    status AS Status,
                    opened_at AS OpenedAt,
                    closed_at AS ClosedAt,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM work_orders;";

            return connection.Query<WorkOrder>(sql);
        }

        public WorkOrder? GetWorkOrderById(int workOrderId)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            const string sql = @"
                SELECT 
                    id AS Id,
                    asset_id AS AssetId,
                    title AS Title,
                    description AS Description,
                    priority AS Priority,
                    status AS Status,
                    opened_at AS OpenedAt,
                    closed_at AS ClosedAt,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM work_orders
                WHERE id = @Id;";

            return connection.QuerySingleOrDefault<WorkOrder>(sql, new { Id = workOrderId });
        }

        public void CreateWorkOrder(CreateWorkOrderRequest workOrder)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            const string sql = @"
                INSERT INTO work_orders (asset_id, title, description, priority, status, opened_at, closed_at, created_at)
                VALUES (@AssetId, @Title, @Description, @Priority, @Status, @OpenedAt, @ClosedAt, @CreatedAt);";

            connection.Execute(sql, new
            {
                AssetId = workOrder.AssetId,
                Title = workOrder.Title,
                Description = workOrder.Description,
                Priority = workOrder.Priority,
                Status = workOrder.Status,
                OpenedAt = workOrder.OpenedAt ?? DateTime.UtcNow,
                ClosedAt = workOrder.ClosedAt,
                CreatedAt = DateTime.UtcNow
            });
        }

        public void UpdateWorkOrderStatus(int workOrderId, string status)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            const string sql = @"
                UPDATE work_orders
                SET status = @Status,
                    updated_at = @UpdatedAt,
                    closed_at = CASE WHEN @Status = 'Closed' THEN @UpdatedAt ELSE closed_at END
                WHERE id = @Id;";

            connection.Execute(sql, new
            {
                Id = workOrderId,
                Status = status,
                UpdatedAt = DateTime.UtcNow
            });
        }

        public void AddLogMessage(int workOrderId, string message)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            const string sql = @"
                INSERT INTO work_order_logs (work_order_id, message, created_at)
                VALUES (@WorkOrderId, @Message, @CreatedAt);";

            connection.Execute(sql, new
            {
                WorkOrderId = workOrderId,
                Message = message,
                CreatedAt = DateTime.UtcNow
            });
        }

        public int CountOpenWorkOrders()
        {
            using var connection = _connectionFactory.CreateConnection();
            string sql = "SELECT COUNT(*) FROM work_orders WHERE status IN ('Open', 'InProgress')";
            return connection.ExecuteScalar<int>(sql);
        }

        public int CountHighPriorityWorkOrders()
        {
            using var connection = _connectionFactory.CreateConnection();
            string sql = "SELECT COUNT(*) FROM work_orders WHERE priority = 'High' AND status != 'Closed'";
            return connection.ExecuteScalar<int>(sql);
        }
    }
}
