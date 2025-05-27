using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.ApplicationLayer.Commands
{
    public class CreateMaintenanceEventCommand
    {
        public long EventId { get; set; }
        public long CarId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public List<PartDTO> Parts { get; set; }

        public class PartDTO
        {
            public string Name { get; set; }
            public string Manufacturer { get; set; }
            public decimal CostValue { get; set; }
            public string Currency { get; set; }
        }
    }
}
