using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Contracts.Requests;

namespace Maintenance___Work_Orders_API.Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly ISqlDriverRepo _sqlDriverRepo;
        public DriverService(ISqlDriverRepo sqlDriverRepo)
        {
            _sqlDriverRepo = sqlDriverRepo;
        }
        public IEnumerable<Driver> GetAllDrivers()
        {
            return _sqlDriverRepo.GetAllDrivers();
        }
        public Driver? GetDriverById(int DriverId)
        {
            return _sqlDriverRepo.GetDriverById(DriverId);
        }
        public Driver CreateDriver(CreateDriverRequest Driver)
        {
            if (!Enum.TryParse<DriverStatus>(Driver.Status, ignoreCase: true, out _))
                throw new ArgumentException("Invalid Driver status");

            return _sqlDriverRepo.CreateDriver(Driver);
        }
        public void UpdateDriver(int DriverId, UpdateDriverRequest Driver)
        {
            if (!Enum.TryParse<DriverStatus>(Driver.Status, ignoreCase: true, out _))
                throw new ArgumentException("Invalid Driver status");

            _sqlDriverRepo.UpdateDriver(DriverId, Driver);
        }
        public void DeleteDriver(int DriverId)
        {
            _sqlDriverRepo.DeleteDriver(DriverId);
        }

    }
}
