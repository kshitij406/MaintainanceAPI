using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Contracts.Requests;

namespace Maintenance___Work_Orders_API.Application.Interfaces
{
    public interface ISqlServiceScheduleRepo
    {
        IEnumerable<ServiceSchedule> GetSchedulesByAsset(int assetId);
        IEnumerable<object> GetOverdueServices(); // Returns a joined list of cars + overdue services
        void CreateSchedule(CreateServiceScheduleRequest request);
        void UpdateServiceHistory(int id, int newOdometer, DateTime newDate);
    }
}