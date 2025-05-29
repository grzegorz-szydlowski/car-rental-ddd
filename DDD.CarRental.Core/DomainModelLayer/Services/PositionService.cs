using System;
using DDD.CarRental.Core.DomainModelLayer.Interfaces;
using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.DomainModelLayer.Services;

public class PositionService : IPositionService
{
    public Position GenerateRandomPosition()
    {
        var random = new Random();
        return new Position(random.NextDouble()*1000, random.NextDouble()*500,"km");
    }
}