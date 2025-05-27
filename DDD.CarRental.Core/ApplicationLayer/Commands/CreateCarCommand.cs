using DDD.CarRental.Core.DomainModelLayer.Models;
using System;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class CreateCarCommand
    {
        public long CarId { get; set; }
        public string RegistrationNumber { get; set; }

        public double InitialDistanceValue { get; set; }
        public string DistanceUnit { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public string PositionUnit { get; set; }
    }
}
