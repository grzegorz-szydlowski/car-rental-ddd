using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public enum CarStatus
    {
        Available = 0,
        Reserved = 1,
        Rented = 2
    }

    public class Car : Entity, IAggregateRoot
    {
        public string RegistrationNumber { get; protected set; }
        public CarStatus Status { get; protected set; }

        public Distance CurrentDistance { get; protected set; }
        public Distance TotalDistance { get; protected set; }
        public Position CurrentPosition { get; protected set; }

        protected Car() { }

        public Car(long id, string registrationNumber, Distance initialDistance, Position initialPosition)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber)) throw new ArgumentException("Invalid registration number.");

            RegistrationNumber = registrationNumber;
            Status = CarStatus.Available;
            CurrentDistance = initialDistance;
            TotalDistance = initialDistance;
            CurrentPosition = initialPosition;
        }

        public void SetAsRented()
        {
            if (Status != CarStatus.Available)
                throw new InvalidOperationException("Car is not available.");
            Status = CarStatus.Rented;
        }

        public void SetAsAvailable()
        {
            if (Status != CarStatus.Rented)
                throw new InvalidOperationException("Car is not rented.");
            Status = CarStatus.Available;
        }

        public void UpdatePosition(Position newPosition)
        {
            var distance = CurrentPosition.CalculateDistance(newPosition);
            CurrentDistance = distance;
            TotalDistance = TotalDistance.Add(distance);
            CurrentPosition = newPosition;
        }
    }
}
