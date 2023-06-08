using Domain;
using Domain.dto;

namespace DAL;

public interface ICarDal
{
    void InsertCar(string email, CarDto car);
    Task<List<CarDto>> GetCarsByEmailAsync(string email);
    Task<CarDto> GetCarByIdAsync(int id);

    Task<bool> DoesCarAlreadyExistAsync(string licenseplate);

    void UpdateCar(Car car);

    void DeleteCar(int id);
}