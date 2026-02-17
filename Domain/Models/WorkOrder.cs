namespace Maintenance___Work_Orders_API.Domain.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public int AssetID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public string? Priority { get; set; }
        public string? WorkOrderStatus { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
