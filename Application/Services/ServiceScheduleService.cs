using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Application.Services
{
    public class ServiceScheduleService : IServiceScheduleService
    {
        private readonly ISqlServiceScheduleRepo _repo;

        public ServiceScheduleService(ISqlServiceScheduleRepo repo)
        {
            _repo = repo;
        }

        public IEnumerable<ServiceSchedule> GetSchedulesByAsset(int assetId) => _repo.GetSchedulesByAsset(assetId);

        public IEnumerable<object> GetOverdueServices() => _repo.GetOverdueServices();

        public void CreateSchedule(CreateServiceScheduleRequest request) => _repo.CreateSchedule(request);

        public void CompleteService(int id, int currentOdometer)
        {

            _repo.UpdateServiceHistory(id, currentOdometer, DateTime.Now);
        }
    }
}