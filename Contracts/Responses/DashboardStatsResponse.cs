namespace Maintenance___Work_Orders_API.Contracts.Responses
{
    public class DashboardStatsResponse
    {
        public int TotalAssets { get; set; }
        public int ActiveAssets { get; set; }
        public int AssetsInMaintenance { get; set; }
        public int OpenWorkOrders { get; set; }
        public int HighPriorityOrders { get; set; }
    }
}
