using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Contracts.Requests;

namespace Maintenance___Work_Orders_API.Application.Interfaces
{
    public interface ISqlFuelLogRepo
    {
        IEnumerable<FuelLog> GetAllFuelLogs();
        IEnumerable<FuelLog> GetFuelLogsByAsset(int assetId);
        void LogFuel(CreateFuelLogRequest request);
    }
}