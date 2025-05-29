using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;

namespace DDD.CarRental.Core.DomainModelLayer.Events;

public class DriverCreatedDomainEvent : DomainEvent
{
    public long DriverId { get; }
    public string LicenceNumber { get; }

    public DriverCreatedDomainEvent(long driverId, string licenceNumber)
    {
        DriverId = driverId;
        LicenceNumber = licenceNumber;
    }
}