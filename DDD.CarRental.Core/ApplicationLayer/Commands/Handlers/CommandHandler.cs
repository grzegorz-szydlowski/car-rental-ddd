using DDD.CarRental.Core.ApplicationLayer.Commands;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.Core.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private readonly ICarRentalUnitOfWork _unitOfWork;
        IPositionService _positionService;

        public CommandHandler(ICarRentalUnitOfWork unitOfWork, IPositionService positionService)
        {
            _unitOfWork = unitOfWork;
            _positionService = positionService;
        }

        public void Execute(CreateCarCommand command)
        {
            var existingCar = _unitOfWork.CarRepository.Get(command.CarId);
            if (existingCar != null)
                throw new InvalidOperationException($"Car with ID {command.CarId} already exists.");

            var initialDistance = new Distance(command.InitialDistanceValue, command.DistanceUnit);
            var initialPosition = new Position(command.PositionX, command.PositionY, command.PositionUnit);

            var car = new Car(command.CarId, command.RegistrationNumber, initialDistance, initialPosition);

            _unitOfWork.CarRepository.Insert(car);
            _unitOfWork.Commit();
        }

        public void Execute(CreateDriverCommand command)
        {
            var existingDriver = _unitOfWork.DriverRepository.Get(command.DriverId);
            if (existingDriver != null)
                throw new InvalidOperationException($"Driver with ID {command.DriverId} already exists.");

            var existingByLicence = _unitOfWork.DriverRepository.GetByLicenceNumber(command.LicenceNumber);
            if (existingByLicence != null)
                throw new InvalidOperationException($"Driver with licence number {command.LicenceNumber} already exists.");

            var driver = new Driver(command.DriverId, command.LicenceNumber, command.FirstName, command.LastName);
            _unitOfWork.DriverRepository.Insert(driver);
            _unitOfWork.Commit();
        }

        public void Execute(StartRentalCommand command)
        {
            var car = _unitOfWork.CarRepository.Get(command.CarId)
                ?? throw new KeyNotFoundException($"Car with ID {command.CarId} not found.");

            var driver = _unitOfWork.DriverRepository.Get(command.DriverId)
                ?? throw new KeyNotFoundException($"Driver with ID {command.DriverId} not found.");

            if (car.Status != CarStatus.Available)
                throw new InvalidOperationException("Car is not available for rental.");

            var rental = new Rental(command.RentalId, car.Id, driver.Id, command.StartTime);
            car.SetAsRented();

            _unitOfWork.RentalRepository.Insert(rental);
            _unitOfWork.Commit();
        }


        public void Execute(EndRentalCommand command)
        {
            var rental = _unitOfWork.RentalRepository.Get(command.RentalId)
                ?? throw new KeyNotFoundException($"Rental {command.RentalId} not found.");

            var car = _unitOfWork.CarRepository.Get(rental.CarId)
                ?? throw new KeyNotFoundException($"Car {rental.CarId} not found.");

            var driver = _unitOfWork.DriverRepository.Get(rental.DriverId)
                ?? throw new KeyNotFoundException($"Driver {rental.DriverId} not found.");

            var newPosition = _positionService.GenerateRandomPosition();

            car.UpdatePosition(newPosition);

            rental.EndRental(command.EndTime, command.PricePerMinute, driver.FreeMinutes);

            driver.AddFreeMinutes(5);

            car.SetAsAvailable();

            _unitOfWork.Commit();
        }

        public void Execute(CreateMaintenanceEventCommand command)
        {
            var car = _unitOfWork.CarRepository.Get(command.CarId)
                ?? throw new KeyNotFoundException($"Car with ID {command.CarId} not found.");

            var maintenance = new MaintenanceEvent(command.EventId, command.CarId, command.Date, command.Description);

            foreach (var part in command.Parts)
            {
                var cost = new Money(part.CostValue, part.Currency);
                maintenance.AddPart(part.Name, part.Manufacturer, cost);
            }

            _unitOfWork.MaintenanceRepository.Insert(maintenance);
            _unitOfWork.Commit();
        }



    }
}
