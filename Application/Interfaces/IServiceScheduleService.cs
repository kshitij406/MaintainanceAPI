using System.Collections.Generic;
using Maintenance___Work_Orders_API.Contracts.Requests;
using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Application.Interfaces
{
    public interface IServiceScheduleService
    {
        IEnumerable<ServiceSchedule> GetSchedulesByAsset(int assetId);
        IEnumerable<object> GetOverdueServices();
        void CreateSchedule(CreateServiceScheduleRequest request);
        void CompleteService(int id, int currentOdometer);
    }
}