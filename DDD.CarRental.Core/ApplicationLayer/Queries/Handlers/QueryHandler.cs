using DDD.CarRental.Core.ApplicationLayer.DTOs;
using DDD.CarRental.Core.ApplicationLayer.Mappers;
using DDD.CarRental.Core.ApplicationLayer.Queries;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDD.CarRental.Core.ApplicationLayer.Queries.Handlers
{
    public class QueryHandler
    {
        private readonly ICarRentalUnitOfWork _unitOfWork;

        public QueryHandler(ICarRentalUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<CarDTO> Handle(GetAllCarsQuery query)
        {
            var cars = _unitOfWork.CarRepository.GetAll();
            return Mapper.Map(cars.ToList());
        }

        public CarDTO Handle(GetCarByIdQuery query)
        {
            var car = _unitOfWork.CarRepository.Get(query.CarId);
            return car != null ? Mapper.Map(car) : null;
        }

        public IList<DriverDTO> Handle(GetAllDriversQuery query)
        {
            var drivers = _unitOfWork.DriverRepository.GetAll();
            return Mapper.Map(drivers.ToList());
        }

        public DriverDTO Handle(GetDriverByIdQuery query)
        {
            var driver = _unitOfWork.DriverRepository.Get(query.DriverId);
            return driver != null ? Mapper.Map(driver) : null;
        }

        public IList<RentalDTO> Handle(GetAllRentalsQuery query)
        {
            var rentals = _unitOfWork.RentalRepository.GetAll();
            return Mapper.Map(rentals.ToList());
        }

        public RentalDTO Handle(GetRentalByIdQuery query)
        {
            var rental = _unitOfWork.RentalRepository.Get(query.RentalId);
            return rental != null ? Mapper.Map(rental) : null;
        }

        public IList<MaintenanceDTO> Handle(GetMaintenanceByCarIdQuery query)
        {
            var events = _unitOfWork.MaintenanceRepository.GetByCarId(query.CarId);
            return Mapper.Map(events.ToList());
        }
    }
}
