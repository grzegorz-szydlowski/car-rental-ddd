namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class CarDTO
    {
        public long Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Status { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public string PositionUnit { get; set; }
    }
}
