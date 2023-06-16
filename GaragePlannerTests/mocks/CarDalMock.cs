using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using Domain.dto;

namespace GaragePlannerTests.mocks
{
    internal class CarDalMock : ICarDal
    {
        private readonly List<CarDto> _cars;
        public bool HasInsertedCar;
        public bool HasDeletedCar;
        public bool HasUpdatedCar;

        public CarDalMock(List<CarDto> cars)
        {
            _cars = cars;
            HasInsertedCar = false;
            HasDeletedCar = false;
            HasUpdatedCar = false;
        }

        public void DeleteCar(int id)
        {
            HasDeletedCar = true;
        }

        public bool DoesCarAlreadyExist(string licenseplate)
        {
            return _cars.Any(car => car.LicensePlate == licenseplate);
        }

        public CarDto GetCarById(int id)
        {
            return _cars.FirstOrDefault(car => car.Id == id);
        }

        public List<CarDto> GetCarsByEmail(string email)
        {
            return _cars;
        }


        public void InsertCar(string email, Car car)
        {
            HasInsertedCar = true;
        }

        public void UpdateCar(Car car)
        {
            HasUpdatedCar = true;
            
        }
    }
}
