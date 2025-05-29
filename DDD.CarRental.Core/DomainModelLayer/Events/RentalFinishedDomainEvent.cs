using System;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Events;

public class RentalFinishedDomainEvent : DomainEvent
{
    public long RentalId { get; }
    public DateTime Finished { get; }

    public RentalFinishedDomainEvent(long rentalId, DateTime finished)
    {
        RentalId = rentalId;
        Finished = finished;
    }
}