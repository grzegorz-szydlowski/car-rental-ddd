using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.ApplicationLayer.DTOs
{
    public class MaintenanceDTO
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
        public string Currency { get; set; }

        public List<PartDTO> Parts { get; set; } = new List<PartDTO>();

        public class PartDTO
        {
            public string Name { get; set; }
            public string Manufacturer { get; set; }
            public decimal Cost { get; set; }
            public string Currency { get; set; }
        }
    }
}
