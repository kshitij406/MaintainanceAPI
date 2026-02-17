using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class CreateAssetRequest
    {
        public string AssetTag { get; set; } = string.Empty;
        public string? PlateNo { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
