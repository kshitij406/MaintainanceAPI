namespace Maintenance___Work_Orders_API.Domain.Models
{
    public class ServiceSchedule
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string ServiceType { get; set; }  = string.Empty;
        public DateTime? LastServiceDate { get; set; }
        public int LastServiceOdometer { get; set; }
        public DateTime? NextDueDate { get; set; }
        public int? NextDueOdometer { get; set; }
    }
}
