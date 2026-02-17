using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Application.Services;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Moq;

namespace MaintenanceAPI.Tests;

public class AssetServiceTests
{
    private readonly Mock<ISqlAssetRepo> _repo = new();

    [Fact]
    public void CreateAsset_WithInvalidStatus_ThrowsArgumentException()
    {
        var service = new AssetService(_repo.Object);
        var request = new CreateAssetRequest
        {
            AssetTag = "A-100",
            Make = "CAT",
            Model = "D8",
            Status = "InvalidStatus"
        };

        Assert.Throws<ArgumentException>(() => service.CreateAsset(request));
    }

    [Fact]
    public void CreateAsset_WithValidStatus_CallsRepository()
    {
        var service = new AssetService(_repo.Object);
        var request = new CreateAssetRequest
        {
            AssetTag = "A-100",
            Make = "CAT",
            Model = "D8",
            Status = "Active"
        };

        service.CreateAsset(request);

        _repo.Verify(r => r.CreateAsset(request), Times.Once);
    }
}
