namespace Maintenance___Work_Orders_API.Domain.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum WorkOrderStatus
    {
        Open,
        InProgress,
        Closed
    }

    public enum AssetStatus 
    { 
        Active, 
        Inactive, 
        Maintenance 
    }
}
