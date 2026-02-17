using System.ComponentModel.DataAnnotations;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class CreateWorkOrderRequest
    {
        [Range(1, int.MaxValue)]
        public int AssetId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string Priority { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        public DateTime? OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
