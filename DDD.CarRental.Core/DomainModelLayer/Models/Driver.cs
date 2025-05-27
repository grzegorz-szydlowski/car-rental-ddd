using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Driver : Entity, IAggregateRoot
    {
        public string LicenceNumber { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        public int FreeMinutes { get; protected set; }

        protected Driver() { }

        public Driver(long id, string licenceNumber, string firstName, string lastName)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(licenceNumber)) throw new ArgumentException("Licence number is required.");
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.");

            LicenceNumber = licenceNumber;
            FirstName = firstName;
            LastName = lastName;
            FreeMinutes = 0;
        }

        public void AddFreeMinutes(int minutes)
        {
            if (minutes < 0) throw new ArgumentException("Cannot add negative minutes.");
            FreeMinutes += minutes;
        }
    }
}
