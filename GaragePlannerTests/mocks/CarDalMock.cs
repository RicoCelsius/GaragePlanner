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
        private bool hasInsertedCar = false;
        private bool hasDeletedCar = false;
        private bool hasUpdatedCar = false;

        public CarDalMock(List<CarDto> cars)
        {
            _cars = cars;
        }

        public void DeleteCar(int id)
        {
            hasDeletedCar = true;
        }

        public bool DoesCarAlreadyExist(string licenseplate)
        {
            throw new NotImplementedException();

        }

        public CarDto GetCarById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CarDto> GetCarsByEmail(string email)
        {
            return _cars;
        }

        public void InsertCar(string email, CarDto car)
        {
            hasInsertedCar = true;
        }

        public void UpdateCar(Car car)
        {
            hasUpdatedCar = true;
            
        }
    }
}
