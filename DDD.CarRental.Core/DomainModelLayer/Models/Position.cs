using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    [Owned]
    public class Position : ValueObject
    {
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public string Unit { get; protected set; } // np. "km", "mi", "m"

        protected Position() { }

        public Position(double x, double y, string unit)
        {
            if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit is required.");
            X = x;
            Y = y;
            Unit = unit;
        }

        public Distance CalculateDistance(Position other)
        {
            if (Unit != other.Unit)
                throw new InvalidOperationException("Cannot calculate distance between positions with different units.");

            double dx = X - other.X;
            double dy = Y - other.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return new Distance(distance, Unit);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
            yield return Unit?.ToLower();
        }

        public override string ToString() => $"({X}, {Y}) {Unit}";
    }
}
