using DDD.SharedKernel.InfrastructureLayer;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Driver GetByLicenceNumber(string licenceNumber);
    }
}
