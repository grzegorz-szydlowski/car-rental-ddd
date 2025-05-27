using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class MaintenanceRepository : Repository<MaintenanceEvent>, IMaintenanceRepository
    {
        public MaintenanceRepository(CarRentalDbContext context) : base(context) { }

        public IList<MaintenanceEvent> GetByCarId(long carId)
        {
            return _context.Set<MaintenanceEvent>()
                .Where(e => e.CarId == carId)
                .ToList();
        }
    }
}
