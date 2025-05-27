namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class CreateDriverCommand
    {
        public long DriverId { get; set; }
        public string LicenceNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
