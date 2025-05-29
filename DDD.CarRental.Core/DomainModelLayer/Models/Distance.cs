using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Distance : ValueObject
    {
        public double Value { get; protected set; }
        public string Unit { get; protected set; } // np. "km", "mi"

        protected Distance() { }

        public Distance(double value, string unit)
        {
            if (value < 0) throw new ArgumentException("Distance cannot be negative.");
            if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit is required.");

            Value = value;
            Unit = unit;
        }

        public Distance Add(Distance other)
        {
            var otherInThisUnit = other.ConvertTo(Unit);
            return new Distance(Value + otherInThisUnit.Value, Unit);
        }

        public Distance Subtract(Distance other)
        {
            var otherInThisUnit = other.ConvertTo(Unit);
            return new Distance(Value - otherInThisUnit.Value, Unit);
        }

        public Distance ConvertTo(string newUnit)
        {
            if (Unit == newUnit) return this;

            if (Unit == "km" && newUnit == "mi")
                return new Distance(Value * 0.621371, "mi");
            if (Unit == "mi" && newUnit == "km")
                return new Distance(Value / 0.621371, "km");

            throw new InvalidOperationException($"Cannot convert from {Unit} to {newUnit}");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Unit?.ToLower();
        }

        public override string ToString() => $"{Value} {Unit}";
    }
}
