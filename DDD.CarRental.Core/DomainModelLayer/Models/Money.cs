using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class Money : ValueObject
    {
        public decimal Value { get; protected set; }
        public string Currency { get; protected set; }

        public static Money Zero => new Money(0, "PLN");

        protected Money() { }

        public Money(decimal value, string currency)
        {
            if (value < 0)
                throw new ArgumentException("Money value cannot be negative.");
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.");

            Value = value;
            Currency = currency.ToUpper();
        }

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Value + other.Value, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Value - other.Value, Currency);
        }

        public Money Multiply(int multiplier)
        {
            return new Money(Value * multiplier, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot operate on different currencies.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }

        public override string ToString() => $"{Value} {Currency}";
    }
}
