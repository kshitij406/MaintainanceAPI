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
            INSERT INTO Assets (asset_tag, plate_no, make, model, status, created_at)
            VALUES 
            (@AssetTag, @PlateNo, @Make, @Model, @Status, @CreatedAt);";

        connection.Execute(sql,
                    new
                    {
                        AssetTag = asset.AssetTag,
                        PlateNo = asset.PlateNo,
                        Make = asset.Make,
                        Model = asset.Model,
                        Status = asset.Status,
                        CreatedAt = DateTime.UtcNow
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
                UpdatedAt = DateTime.UtcNow
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
    
}
