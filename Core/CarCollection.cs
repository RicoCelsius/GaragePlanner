using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain.dto;
using Domain.interfaces;
using Domain.utils;
using MySqlConnector;

namespace Domain
{
    public class CarCollection
    {
        private ICarDal _iCarDal;

        public CarCollection(ICarDal iCarDal)
        {
            _iCarDal = iCarDal;
        }

        public Result CreateCar(string email, Car car)
        {
            if (_iCarDal.DoesCarAlreadyExist(car.LicensePlate))
            {
                return new Result(false, "License plate already exists");
            }
            _iCarDal.InsertCar(email,car);
            return new Result(true, "Car created");
        }

        public void DeleteCar(int id)
        {
            _iCarDal.DeleteCar(id);
        }

        public void EditCar(Car car)
        {
            _iCarDal.UpdateCar(car);
        }

        public Car GetCarById(int id)
        {
            CarDto carDto = _iCarDal.GetCarById(id);
            Car car = DtoConverter.ConvertCarDtoToCar(carDto);
            return car;
        }


        public List<Car> GetCustomerCarsByCustomerEmail(string email)
        {
            List<CarDto> customerCarsDto = _iCarDal.GetCarsByEmail(email);
            List<Car> customerCars = new List<Car>();

            foreach (CarDto car in customerCarsDto)
            {
                customerCars.Add(DtoConverter.ConvertCarDtoToCar(car));
            }
           
            return customerCars;
        }

    }
}
