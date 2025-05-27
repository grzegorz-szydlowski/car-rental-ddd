using DDD.SharedKernel.InfrastructureLayer;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Car GetByRegistrationNumber(string registrationNumber);
    }
}
