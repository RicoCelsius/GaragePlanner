using Domain;
using Domain.dto;

namespace DAL;

public interface ICarDal
{
    void InsertCar(string email, Car car);
    List<CarDto> GetCarsByEmail(string email);
    CarDto GetCarByLicensePlate(string licensePlate);

    void UpdateCar(Car car);
    void DeleteCar(int id);
}