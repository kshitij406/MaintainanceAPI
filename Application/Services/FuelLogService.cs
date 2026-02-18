using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Application.Services
{
    public class FuelLogService : IFuelLogService
    {
        private readonly ISqlFuelLogRepo _repo;

        public FuelLogService(ISqlFuelLogRepo repo)
        {
            _repo = repo;
        }

        public IEnumerable<FuelLog> GetAllFuelLogs() => _repo.GetAllFuelLogs();
        public IEnumerable<FuelLog> GetFuelLogsByAsset(int assetId) => _repo.GetFuelLogsByAsset(assetId);
        public void LogFuel(CreateFuelLogRequest request) => _repo.LogFuel(request);
    }
}