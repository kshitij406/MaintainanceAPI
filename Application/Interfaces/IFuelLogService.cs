using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Application.Interfaces
{
    public interface IFuelLogService
    {
        IEnumerable<FuelLog> GetAllFuelLogs();
        IEnumerable<FuelLog> GetFuelLogsByAsset(int assetId);
        void LogFuel(CreateFuelLogRequest request);
    }
}