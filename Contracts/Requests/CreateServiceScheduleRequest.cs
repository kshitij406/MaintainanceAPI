namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class CreateServiceScheduleRequest
    {
        public int AssetId { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public DateTime? LastServiceDate { get; set; }
        public int LastServiceOdometer { get; set; }
        public DateTime? NextDueDate { get; set; }
        public int? NextDueOdometer { get; set; }
    }
}