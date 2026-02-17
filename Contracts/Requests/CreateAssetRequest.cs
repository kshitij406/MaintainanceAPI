using System.ComponentModel.DataAnnotations;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class CreateAssetRequest
    {
        [Required]
        public string AssetTag { get; set; } = string.Empty;

        public string? PlateNo { get; set; }

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
