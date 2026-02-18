using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Application.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly ISqlWorkOrderRepo _workOrderRepo;

        public WorkOrderService(ISqlWorkOrderRepo workOrderRepo)
        {
            _workOrderRepo = workOrderRepo;
        }

        public IEnumerable<WorkOrder> GetAllWorkOrders()
        {
            return _workOrderRepo.GetAllWorkOrders();
        }

        public WorkOrder? GetWorkOrderById(int workOrderId)
        {
            return _workOrderRepo.GetWorkOrderById(workOrderId);
        }

        public void CreateWorkOrder(CreateWorkOrderRequest workOrder)
        {
            if (!Enum.TryParse<WorkOrderStatus>(workOrder.Status, ignoreCase: true, out _))
                throw new ArgumentException("Invalid work order status");

            if (!Enum.TryParse<Priority>(workOrder.Priority, ignoreCase: true, out _))
                throw new ArgumentException("Invalid work order priority");

            _workOrderRepo.CreateWorkOrder(workOrder);
        }

        public void UpdateWorkOrderStatus(int workOrderId, string status)
        {
            if (!Enum.TryParse<WorkOrderStatus>(status, ignoreCase: true, out _))
                throw new ArgumentException("Invalid work order status");

            _workOrderRepo.UpdateWorkOrderStatus(workOrderId, status);
        }

        public void AddLogMessage(int workOrderId, string message)
        {
            _workOrderRepo.AddLogMessage(workOrderId, message);
        }
        
    }
}
