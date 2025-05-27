using DDD.CarRental.Core.ApplicationLayer.DTOs;
using DDD.CarRental.Core.DomainModelLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DDD.CarRental.Core.ApplicationLayer.Mappers
{
    public static class Mapper
    {
        public static CarDTO Map(Car car)
        {
            return new CarDTO
            {
                Id = car.Id,
                RegistrationNumber = car.RegistrationNumber,
                Status = car.Status.ToString(),
                PositionX = car.CurrentPosition.X,
                PositionY = car.CurrentPosition.Y,
                PositionUnit = car.CurrentPosition.Unit
            };
        }

        public static DriverDTO Map(Driver driver)
        {
            return new DriverDTO
            {
                Id = driver.Id,
                LicenceNumber = driver.LicenceNumber,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                FreeMinutes = driver.FreeMinutes
            };
        }

        public static RentalDTO Map(Rental rental)
        {
            return new RentalDTO
            {
                Id = rental.Id,
                CarId = rental.CarId,
                DriverId = rental.DriverId,
                Started = rental.Started,
                Finished = rental.Finished,
                TotalAmount = rental.Total?.Value ?? 0,
                TotalCurrency = rental.Total?.Currency ?? "PLN"
            };
        }



        public static List<CarDTO> Map(List<Car> cars)
        {
            return cars.Select(Map).ToList();
        }

        public static List<DriverDTO> Map(List<Driver> drivers)
        {
            return drivers.Select(Map).ToList();
        }

        public static List<RentalDTO> Map(List<Rental> rentals)
        {
            return rentals.Select(Map).ToList();
        }


        public static MaintenanceDTO Map(MaintenanceEvent m)
        {
            var dto = new MaintenanceDTO
            {
                Id = m.Id,
                CarId = m.CarId,
                Date = m.Date,
                Description = m.Description,
                TotalCost = m.Cost.Value,
                Currency = m.Cost.Currency
            };

            foreach (var part in m.Parts)
            {
                dto.Parts.Add(new MaintenanceDTO.PartDTO
                {
                    Name = part.Name,
                    Manufacturer = part.Manufacturer,
                    Cost = part.Cost.Value,
                    Currency = part.Cost.Currency
                });
            }

            return dto;
        }

        public static List<MaintenanceDTO> Map(List<MaintenanceEvent> events)
        {
            return events.Select(Map).ToList();
        }
    }
}
