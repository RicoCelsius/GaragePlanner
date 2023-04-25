using Domain;

namespace DAL;

public interface ICarDal
{
    void InsertCar(Car car);
    List<Car> GetCarsByCustomerId(int customerId);
    Car GetCarByLicensePlate(string licensePlate);
}