using DDD.CarRental.Core.DomainModelLayer.Models;

namespace DDD.CarRental.Core.DomainModelLayer.Interfaces;

public interface IPositionService
{
    public Position GenerateRandomPosition();
}