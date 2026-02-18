using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Application.Interfaces
{
    public interface ISqlWorkOrderRepo
    {
        IEnumerable<WorkOrder> GetAllWorkOrders();
        WorkOrder? GetWorkOrderById(int workOrderId);
        void CreateWorkOrder(CreateWorkOrderRequest workOrder);
        void UpdateWorkOrderStatus(int workOrderId, string status);
        void AddLogMessage(int workOrderId, string message);
        int CountOpenWorkOrders();
        int CountHighPriorityWorkOrders();
    }
}
