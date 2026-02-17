using System.ComponentModel.DataAnnotations;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class UpdateAssetRequest
    {
        [Required]
        public string AssetTag { get; set; } = string.Empty;
        public string PlateNo { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
