using Dapper;
using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Infrastructure.DB;

namespace Maintenance___Work_Orders_API.Infrastructure.Repository
{
    public class SqlServiceScheduleRepo : ISqlServiceScheduleRepo
    {
        private readonly IDbConnectionFactory _db;

        public SqlServiceScheduleRepo(IDbConnectionFactory db)
        {
            _db = db;
        }

        public IEnumerable<ServiceSchedule> GetSchedulesByAsset(int assetId)
        {
            using var conn = _db.CreateConnection();
            return conn.Query<ServiceSchedule>("SELECT * FROM service_schedules WHERE asset_id = @Id", new { Id = assetId });
        }

        public IEnumerable<object> GetOverdueServices()
        {
            using var conn = _db.CreateConnection();
            // This JOIN is what makes the app "Smart". 
            // It compares the Schedule's due limit vs the Asset's actual mileage.
            string sql = @"
                SELECT 
                    a.asset_tag, a.make, a.model, a.odometer, 
                    s.service_type, s.next_due_odometer, s.next_due_date
                FROM service_schedules s
                JOIN assets a ON s.asset_id = a.id
                WHERE 
                    (s.next_due_odometer IS NOT NULL AND a.odometer >= s.next_due_odometer)
                    OR 
                    (s.next_due_date IS NOT NULL AND NOW() >= s.next_due_date)";

            return conn.Query<object>(sql);
        }

        public void CreateSchedule(CreateServiceScheduleRequest req)
        {
            using var conn = _db.CreateConnection();
            string sql = @"
                INSERT INTO service_schedules 
                (asset_id, service_type, last_service_date, last_service_odometer, next_due_date, next_due_odometer)
                VALUES 
                (@AssetId, @ServiceType, @LastServiceDate, @LastServiceOdometer, @NextDueDate, @NextDueOdometer)";
            conn.Execute(sql, req);
        }

        public void UpdateServiceHistory(int id, int newOdometer, DateTime newDate)
        {
            using var conn = _db.CreateConnection();
            string sql = @"
                UPDATE service_schedules 
                SET last_service_date = @Date, 
                    last_service_odometer = @Odo,
                    next_due_odometer = @Odo + 5000, -- Auto-schedule next one for 5000km later
                    next_due_date = DATE_ADD(@Date, INTERVAL 3 MONTH)
                WHERE id = @Id";
            conn.Execute(sql, new { Id = id, Odo = newOdometer, Date = newDate });
        }
    }
}