using System.ComponentModel.DataAnnotations;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class UpdateDriverRequest
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
}
