namespace Maintenance___Work_Orders_API.Contracts.Requests
{
    public class CreateFuelLogRequest
    {
        public int AssetId { get; set; }
        public int? DriverId { get; set; } 
        public int OdometerReading { get; set; }
        public decimal Liters { get; set; }
        public decimal PricePerLiter { get; set; }
       
    }
}