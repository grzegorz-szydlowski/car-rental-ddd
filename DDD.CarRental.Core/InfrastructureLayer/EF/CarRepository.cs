using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System.Linq;

namespace DDD.CarRental.Core.InfrastructureLayer.EF
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(CarRentalDbContext context) : base(context) { }

        public Car GetByRegistrationNumber(string registrationNumber)
        {
            return _context.Cars.FirstOrDefault(c => c.RegistrationNumber == registrationNumber);
        }
    }
}
