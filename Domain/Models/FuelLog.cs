namespace Maintenance___Work_Orders_API.Domain.Models
{
    public class FuelLog
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int? DriverId { get; set; } 
        public int OdometerReading { get; set; }
        public decimal Liters { get; set; } 
        public decimal PricePerLiter { get; set; }
        public decimal TotalCost { get; set; }

        public DateTime FillDate { get; set; }
    }
}