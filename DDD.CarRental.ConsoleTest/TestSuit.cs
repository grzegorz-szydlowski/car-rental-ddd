using DDD.CarRental.Core.ApplicationLayer.Commands;
using DDD.CarRental.Core.ApplicationLayer.Commands.Handlers;
using DDD.CarRental.Core.ApplicationLayer.DTOs;
using DDD.CarRental.Core.ApplicationLayer.Queries;
using DDD.CarRental.Core.ApplicationLayer.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DDD.CarRental.ConsoleTest
{
    public class TestSuit
    {
        private IServiceProvider _serviceProvide;
        private CommandHandler _commandHandler;
        private QueryHandler _queryHandler;

        public TestSuit(IServiceCollection serviceCollection)
        {
            _serviceProvide = serviceCollection.BuildServiceProvider();

            _commandHandler = _serviceProvide.GetRequiredService<CommandHandler>();
            _queryHandler = _serviceProvide.GetRequiredService<QueryHandler>();
        }

        public void Run()
        {
            Console.WriteLine("=== Car Rental Test Scenario ===");

            // 1. Create Driver
            var createDriver = new CreateDriverCommand
            {
                DriverId = 1,
                LicenceNumber = "XYZ12345",
                FirstName = "Anna",
                LastName = "Nowak"
            };
            _commandHandler.Execute(createDriver);
            Console.WriteLine("Driver created.");

            // 2. Create Car
            var createCar = new CreateCarCommand
            {
                CarId = 100,
                RegistrationNumber = "KR1CAR01",
                InitialDistanceValue = 0,
                DistanceUnit = "km",
                PositionX = 0,
                PositionY = 0,
                PositionUnit = "km"
            };
            _commandHandler.Execute(createCar);
            Console.WriteLine("Car created.");

            // 3. Start Rental
            var startRental = new StartRentalCommand
            {
                RentalId = 500,
                CarId = 100,
                DriverId = 1,
                StartTime = DateTime.Now
            };
            _commandHandler.Execute(startRental);
            Console.WriteLine("Rental started.");

            // 4. End Rental
            var endRental = new EndRentalCommand
            {
                RentalId = 500,
                EndTime = DateTime.Now.AddMinutes(30),
                PositionX = 3,
                PositionY = 4,
                PositionUnit = "km",
                PricePerMinute = 1.5m
            };
            _commandHandler.Execute(endRental);
            Console.WriteLine("Rental ended.");

            // 5. Create Maintenance Event
            var maintenanceCommand = new CreateMaintenanceEventCommand
            {
                EventId = 900,
                CarId = 100,
                Date = DateTime.Now,
                Description = "Oil change + brake pads",
                Parts = new List<CreateMaintenanceEventCommand.PartDTO>
                {
                    new CreateMaintenanceEventCommand.PartDTO
                    {
                        Name = "Oil filter",
                        Manufacturer = "Bosch",
                        CostValue = 40,
                        Currency = "PLN"
                    },
                    new CreateMaintenanceEventCommand.PartDTO
                    {
                        Name = "Brake pads",
                        Manufacturer = "TRW",
                        CostValue = 120,
                        Currency = "PLN"
                    }
                }
            };
            _commandHandler.Execute(maintenanceCommand);
            Console.WriteLine("Maintenance recorded.");

            // 6. Show results

            var car = _queryHandler.Handle(new GetCarByIdQuery { CarId = 100 });
            Console.WriteLine($"\nCar: {car.RegistrationNumber}, Status: {car.Status}, Position: ({car.PositionX}, {car.PositionY})");

            var driver = _queryHandler.Handle(new GetDriverByIdQuery { DriverId = 1 });
            Console.WriteLine($"Driver: {driver.FirstName} {driver.LastName}, FreeMinutes: {driver.FreeMinutes}");

            var rental = _queryHandler.Handle(new GetRentalByIdQuery { RentalId = 500 });
            Console.WriteLine($"Rental: Started at {rental.Started}, Finished at {rental.Finished}, Cost: {rental.TotalAmount} {rental.TotalCurrency}");

            var maintenanceEvents = _queryHandler.Handle(new GetMaintenanceByCarIdQuery { CarId = 100 });
            foreach (var me in maintenanceEvents)
            {
                Console.WriteLine($"Maintenance on {me.Date}: {me.Description}, Total: {me.TotalCost} {me.Currency}");
                foreach (var part in me.Parts)
                {
                    Console.WriteLine($"  - {part.Name} by {part.Manufacturer}: {part.Cost} {part.Currency}");
                }
            }

            Console.WriteLine("\n=== END ===");
        }
    }
}
