using Dapper;
using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Infrastructure.DB;
using Maintenance___Work_Orders_API.Contracts.Requests;

public class SqlAssetRepo : ISqlAssetRepo
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SqlAssetRepo(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<Asset> GetAllAssets()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"
            SELECT 
                id AS Id,
                asset_tag AS AssetTag,
                plate_no AS PlateNo,
                make AS Make,
                model AS Model,
                status AS Status,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
                current_driver_id AS CurrentDriverId
            FROM Assets;
            ";

        return connection.Query<Asset>(sql);
    }

    public Asset? GetAssetById(int assetId)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"SELECT 
        id AS Id,
        asset_tag AS AssetTag,
        plate_no AS PlateNo,
        make AS Make,
        model AS Model,
        status AS Status,
        odometer AS Odometer,
        current_driver_id AS CurrentDriverId
        created_at AS CreatedAt,
        updated_at AS UpdatedAt
        
        FROM Assets
        WHERE id = @Id";

        Asset? asset = connection.QuerySingleOrDefault<Asset>(sql, new { Id = assetId });
        return asset;
    }


    public void CreateAsset(CreateAssetRequest asset)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"
            INSERT INTO Assets (asset_tag, plate_no, make, model, status, odometer, current_driver_id)
            VALUES 
            (@AssetTag, @PlateNo, @Make, @Model, @Status, @Odometer, @CurrentDriverId);";

        connection.Execute(sql,
                    new
                    {
                        AssetTag = asset.AssetTag,
                        PlateNo = asset.PlateNo,
                        Make = asset.Make,
                        Model = asset.Model,
                        Status = asset.Status,
                        Odometer = asset.Odometer,
                        CurrentDriverId = asset.CurrentDriverId
                    }); 
    }

    public void UpdateAsset(int assetId, UpdateAssetRequest asset)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"
            UPDATE Assets
            SET asset_tag = @AssetTag,
                plate_no = @PlateNo,
                make = @Make,
                model = @Model,
                status = @Status,
                odometer = @Odometer,
                current_driver_id = @CurrentDriverId ,
                updated_at = @UpdatedAt
            WHERE id = @Id
        ";
        
        connection.Execute(sql,
            new
            {
                Id = assetId,
                AssetTag = asset.AssetTag,
                PlateNo = asset.PlateNo,
                Make = asset.Make,
                Model = asset.Model,
                Status = asset.Status,
                Odometer = asset.Odometer,
                CurrentDriverId = asset.CurrentDriverId,
                UpdatedAt = DateTime.UtcNow,
            });
    }
    public void DeleteAsset(int assetId)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        const string sql = "DELETE FROM Assets WHERE id = @Id";

        connection.Execute(sql,
            new
            {
                Id = assetId
            });

    }

    public void AssignDriver(int assetId, int driverId)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        const string sql = @"
            UPDATE Assets
            SET current_driver_id = @DriverId,
                updated_at = @UpdatedAt
            WHERE id = @AssetId
        ";
        connection.Execute(sql,
            new
            {
                AssetId = assetId,
                DriverId = driverId,
                UpdatedAt = DateTime.UtcNow
            });

        const string updateDriverSql = @"
            UPDATE Drivers
            SET status = 'Active'
            WHERE id = @DriverId
        ";

        connection.Execute(updateDriverSql, new { DriverId = driverId });
    }

    public int CountAssets()
    {
        using var connection = _connectionFactory.CreateConnection();
        return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM assets");
    }

    public int CountAssetsByStatus(string status)
    {
        using var connection = _connectionFactory.CreateConnection();
        string sql = "SELECT COUNT(*) FROM assets WHERE status = @Status";
        return connection.ExecuteScalar<int>(sql, new { Status = status });
    }

}
