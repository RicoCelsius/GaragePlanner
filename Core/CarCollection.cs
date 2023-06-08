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
        private readonly ICarDal _iCarDal;

        public CarCollection(ICarDal iCarDal)
        {
            _iCarDal = iCarDal;
        }

        public async Task<Result> CreateCarAsync(string email, Car car)
        {
            if (await _iCarDal.DoesCarAlreadyExistAsync(car.LicensePlate))
            {
                return new Result(false, "License plate already exists");
            }

            CarDto carDto = DomainConverter.ConvertCarToCarDto(car);
            _iCarDal.InsertCar(email,carDto);
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

        public async Task<Car> GetCarByIdAsync(int id)
        {
            CarDto carDto = await _iCarDal.GetCarByIdAsync(id);
            Car car = DtoConverter.ConvertCarDtoToCar(carDto);
            return car;
        }


        public async Task<List<Car>> GetCustomerCarsByCustomerEmailAsync(string email)
        {
            List<CarDto> customerCarsDto = await _iCarDal.GetCarsByEmailAsync(email);
            List<Car> customerCars = new List<Car>();

            foreach (CarDto car in customerCarsDto)
            {
                customerCars.Add(DtoConverter.ConvertCarDtoToCar(car));
            }
           
            return customerCars;
        }

    }
}
