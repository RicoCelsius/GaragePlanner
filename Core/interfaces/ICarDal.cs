using Domain;
using Domain.dto;

namespace DAL;

public interface ICarDal
{
    void InsertCar(int? CustomerId, Car car);
    List<CarDto> GetCarsByEmail(string email);
    CarDto GetCarByLicensePlate(string licensePlate);
}