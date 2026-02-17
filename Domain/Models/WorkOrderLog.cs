namespace Maintenance___Work_Orders_API.Domain.Models
{
    public class WorkOrderLog
    {
        public int Id { get; set; }
        public int WorkOrderID { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
