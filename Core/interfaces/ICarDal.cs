using Domain;
using Domain.dto;

namespace DAL;

public interface ICarDal
{
    void InsertCar(int? CustomerId, Car car);
    List<CarDto> GetCarsByCustomerId(int customerId);
    CarDto GetCarByLicensePlate(string licensePlate);
}