using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Contracts.Requests;

namespace Maintenance___Work_Orders_API.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly ISqlAssetRepo _sqlAssetRepo;
        public AssetService(ISqlAssetRepo sqlAssetRepo)
        {
            _sqlAssetRepo = sqlAssetRepo;
        }
        public IEnumerable<Asset> GetAllAssets()
        {
            return _sqlAssetRepo.GetAllAssets();
        }
        public Asset? GetAssetById(int assetId)
        {
            return _sqlAssetRepo.GetAssetById(assetId);
        }
        public void CreateAsset(CreateAssetRequest asset)
        {
            if (!Enum.TryParse<AssetStatus>(asset.Status, ignoreCase: true, out _))
                throw new ArgumentException("Invalid asset status");

            _sqlAssetRepo.CreateAsset(asset);
        }
        public void UpdateAsset(int assetId, UpdateAssetRequest asset)
        {
            if (!Enum.TryParse<AssetStatus>(asset.Status, ignoreCase: true, out _))
                throw new ArgumentException("Invalid asset status");

            _sqlAssetRepo.UpdateAsset(assetId, asset);
        }
        public void DeleteAsset(int assetId) 
        {
            _sqlAssetRepo.DeleteAsset(assetId);
        }

    }
}
