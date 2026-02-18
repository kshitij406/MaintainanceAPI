using System.ComponentModel.DataAnnotations;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class CreateDriverRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string LicenseNumber { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
}