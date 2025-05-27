using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Rental : Entity, IAggregateRoot
    {
        public long CarId { get; protected set; }
        public long DriverId { get; protected set; }

        public DateTime Started { get; protected set; }
        public DateTime? Finished { get; protected set; }

        public Money Total { get; protected set; }

        protected Rental() { }

        public Rental(long id, long carId, long driverId, DateTime started)
            : base(id)
        {
            CarId = carId;
            DriverId = driverId;
            Started = started;
            Total = Money.Zero;
        }

        public void EndRental(DateTime endTime, decimal pricePerMinute, int freeMinutes)
        {
            if (Finished.HasValue)
                throw new InvalidOperationException("Rental is already ended.");

            if (endTime < Started)
                throw new ArgumentException("End time cannot be before start time.");

            Finished = endTime;

            var totalMinutes = (int)Math.Ceiling((Finished.Value - Started).TotalMinutes);
            var chargeableMinutes = Math.Max(0, totalMinutes - freeMinutes);
            Total = new Money(chargeableMinutes * pricePerMinute, "PLN");
        }
    }
}
