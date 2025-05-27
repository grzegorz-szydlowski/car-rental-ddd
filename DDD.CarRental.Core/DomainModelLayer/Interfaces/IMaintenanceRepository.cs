using DDD.CarRental.Core.DomainModelLayer.Models;
using DDD.SharedKernel.InfrastructureLayer;
using System.Collections.Generic;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IMaintenanceRepository : IRepository<MaintenanceEvent>
    {
        IList<MaintenanceEvent> GetByCarId(long carId);
    }
}
