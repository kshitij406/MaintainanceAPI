using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Application.Services;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Moq;

namespace MaintenanceAPI.Tests;

public class WorkOrderServiceTests
{
    private readonly Mock<ISqlWorkOrderRepo> _repo = new();

    [Fact]
    public void CreateWorkOrder_WithInvalidPriority_ThrowsArgumentException()
    {
        var service = new WorkOrderService(_repo.Object);
        var request = new CreateWorkOrderRequest
        {
            AssetId = 1,
            Title = "Fix brakes",
            Priority = "Emergency",
            Status = "Open"
        };

        Assert.Throws<ArgumentException>(() => service.CreateWorkOrder(request));
    }

    [Fact]
    public void UpdateWorkOrderStatus_WithInvalidStatus_ThrowsArgumentException()
    {
        var service = new WorkOrderService(_repo.Object);

        Assert.Throws<ArgumentException>(() => service.UpdateWorkOrderStatus(1, "Done"));
    }
}
