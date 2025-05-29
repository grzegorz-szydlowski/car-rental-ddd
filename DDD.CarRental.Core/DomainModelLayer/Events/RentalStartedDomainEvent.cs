using System;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Events;

public class RentalStartedDomainEvent : DomainEvent
{
    public long RentalId { get; }
    public DateTime Started { get; }

    public RentalStartedDomainEvent(long rentalId, DateTime started)
    {
        RentalId = rentalId;
        Started = started;
    }
}