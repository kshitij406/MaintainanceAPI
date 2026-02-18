using Dapper;
using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Infrastructure.DB;

namespace Maintenance___Work_Orders_API.Infrastructure.Repository
{
    public class SqlFuelLogRepo : ISqlFuelLogRepo
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public SqlFuelLogRepo(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<FuelLog> GetAllFuelLogs()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return connection.Query<FuelLog>("SELECT * FROM fuel_logs ORDER BY fill_date DESC");
        }

        public IEnumerable<FuelLog> GetFuelLogsByAsset(int assetId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return connection.Query<FuelLog>("SELECT * FROM fuel_logs WHERE asset_id = @AssetId ORDER BY fill_date DESC", new { AssetId = assetId });
        }

        public void LogFuel(CreateFuelLogRequest request)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var sql = @"
                INSERT INTO fuel_logs (asset_id, driver_id, odometer_reading, liters, price_per_liter, fill_date) 
                VALUES (@AssetId, @DriverId, @OdometerReading, @Liters, @PricePerLiter, NOW());
                
                -- Optimization: Auto-update the vehicle's odometer if this reading is higher
                UPDATE assets 
                SET odometer = @OdometerReading 
                WHERE id = @AssetId AND odometer < @OdometerReading;
            ";

            connection.Execute(sql, request);
        }
    }
}