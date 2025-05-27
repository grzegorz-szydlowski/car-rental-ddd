using DDD.SharedKernel.DomainModelLayer.Implementations;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class PartReplaced : ValueObject
    {
        public string Name { get; protected set; }
        public string Manufacturer { get; protected set; }
        public Money Cost { get; protected set; }

        protected PartReplaced() { }

        public PartReplaced(string name, string manufacturer, Money cost)
        {
            Name = name;
            Manufacturer = manufacturer;
            Cost = cost;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name.ToUpper();
            yield return Manufacturer.ToUpper();
            yield return Cost;
        }
    }
}
