using System;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class EndRentalCommand
    {
        public long RentalId { get; set; }
        public DateTime EndTime { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public string PositionUnit { get; set; }

        public decimal PricePerMinute { get; set; }
    }
}
