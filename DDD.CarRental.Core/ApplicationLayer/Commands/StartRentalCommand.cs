using System;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class StartRentalCommand
    {
        public long RentalId { get; set; }
        public long CarId { get; set; }
        public long DriverId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
