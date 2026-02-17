using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Contracts.Requests;

namespace Maintenance___Work_Orders_API.Application.Interfaces
{
    public interface IAssetService
    {
        IEnumerable<Asset> GetAllAssets();
        Asset? GetAssetById(int assetId);
        void CreateAsset(CreateAssetRequest asset);
        void UpdateAsset(int assetId, UpdateAssetRequest asset);
        void DeleteAsset(int assetId);
    }
}
