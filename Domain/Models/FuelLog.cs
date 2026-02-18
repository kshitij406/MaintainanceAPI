namespace Maintenance___Work_Orders_API.Domain.Models
{
    public class FuelLog
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int DriverId { get; set; }
        public int OdometerReading { get; set; }
        public decimal liters { get; set; }
        public decimal PricePerLiter { get; set; }
        public decimal Quantity => liters;
        public decimal TotalCost => Quantity * PricePerLiter;
        public DateTime FillDate { get; set; }
    }
}
