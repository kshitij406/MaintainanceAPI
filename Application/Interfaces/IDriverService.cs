using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Contracts.Requests;

namespace Maintenance___Work_Orders_API.Application.Interfaces
{
    public interface IDriverService
    {
        IEnumerable<Driver> GetAllDrivers();
        Driver? GetDriverById(int id);
        Driver CreateDriver(CreateDriverRequest request);
        void UpdateDriver(int id, UpdateDriverRequest request);
        void DeleteDriver(int id);
    }
}