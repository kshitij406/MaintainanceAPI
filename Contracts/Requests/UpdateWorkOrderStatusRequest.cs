using System.ComponentModel.DataAnnotations;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class UpdateWorkOrderStatusRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
