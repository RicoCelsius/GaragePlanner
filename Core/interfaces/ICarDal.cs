using Domain;
using Domain.dto;

namespace DAL;

public interface ICarDal
{
    void InsertCar(string email, CarDto car);
    List<CarDto> GetCarsByEmail(string email);
    CarDto GetCarById(int id);

    bool DoesCarAlreadyExist(string licenseplate);

    void UpdateCar(Car car);

    void DeleteCar(int id);
}