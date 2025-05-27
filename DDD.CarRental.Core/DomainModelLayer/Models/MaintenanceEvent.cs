using DDD.SharedKernel.DomainModelLayer;
using DDD.SharedKernel.DomainModelLayer.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDD.CarRental.Core.DomainModelLayer.Models
{
    public class MaintenanceEvent : Entity, IAggregateRoot
    {
        public long CarId { get; protected set; }
        public DateTime Date { get; protected set; }
        public string Description { get; protected set; }
        public Money Cost { get; protected set; }

        protected List<PartReplaced> _parts = new List<PartReplaced>();
        public IReadOnlyCollection<PartReplaced> Parts => _parts.AsReadOnly();

        protected MaintenanceEvent() { }

        public MaintenanceEvent(long id, long carId, DateTime date, string description)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.");

            CarId = carId;
            Date = date;
            Description = description;
            Cost = Money.Zero;
        }

        public void AddPart(string name, string manufacturer, Money cost)
        {
            var part = new PartReplaced(name, manufacturer, cost);
            _parts.Add(part);
            Cost = Cost.Add(cost);
        }

        public void AddPart(PartReplaced part)
        {
            _parts.Add(part);
            Cost = Cost.Add(part.Cost);
        }
    }
}
