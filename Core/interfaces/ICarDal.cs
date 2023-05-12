using Domain;
using Domain.dto;

namespace DAL;

public interface ICarDal
{
    void InsertCar(Car car);
    List<CarDto> GetCarsByCustomerId(int customerId);
    CarDto GetCarByLicensePlate(string licensePlate);
}