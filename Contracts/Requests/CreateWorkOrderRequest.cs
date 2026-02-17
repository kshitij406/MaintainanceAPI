using Maintenance___Work_Orders_API.Domain.Models;

namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class CreateWorkOrderRequest
    {
        public int AssetID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public string? Priority {  get; set; }
        public string? Status { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
