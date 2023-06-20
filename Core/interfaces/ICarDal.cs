using Domain;
using Domain.dto;

namespace DAL;

public interface ICarDal
{
    void InsertCar(string email, Car car);
    List<CarDto> GetCarsByEmail(string email);
    CarDto GetCarById(int id);

    List<string> GetBrands();
    bool DoesCarAlreadyExist(string licenseplate);
    void DeleteBrand(string brand);
    void InsertBrand(string brand);

    void UpdateCar(Car car);

    void DeleteCar(int id);
}