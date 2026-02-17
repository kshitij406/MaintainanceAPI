using System.ComponentModel.DataAnnotations;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class AddWorkOrderLogRequest
    {
        [Required]
        [MinLength(2)]
        public string Message { get; set; } = string.Empty;
    }
}
