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

        public bool TryCreateCar(string email, string licensePlate, string model, Enums.Color color, int year)
        {

            Car car = new Car(licensePlate,color,model, year);
            try
            {
                if (_iCarDal.DoesCarAlreadyExist(car.LicensePlate))
                {
                    return false;
                }



                _iCarDal.InsertCar(email, car);
            }
            catch (Exception e)
            {
                throw new DalException("Could not create car", e);
            }

            return true;
        }

        public void DeleteCar(int id)
        {
            try
            {
                _iCarDal.DeleteCar(id);
            }
            catch (Exception e)
            {
                throw new DalException("Could not delete car", e);
            }
        }

        public void EditCar(Car car)
        {
            try
            {
                _iCarDal.UpdateCar(car);
            }
            catch (Exception e)
            {
                throw new DalException("Could not edit car", e);
            }
        }

        public Car GetCarById(int id)
        {
            try
            {
                CarDto carDto = _iCarDal.GetCarById(id);
                Car car = DtoConverter.ConvertCarDtoToCar(carDto);
                return car;
            }
            catch (Exception e)
            {
                throw new DalException("Could not get car", e);
            }
        }


        public List<Car> GetCustomerCarsByCustomerEmail(string email)
        {
            List<CarDto> customerCarsDto;
            try
            {
                customerCarsDto = _iCarDal.GetCarsByEmail(email);
            }
            catch (Exception e)
            {
                throw new DalException("Could not get cars", e);
            }

            List<Car> customerCars = new List<Car>();

            foreach (CarDto car in customerCarsDto)
            {
                customerCars.Add(DtoConverter.ConvertCarDtoToCar(car));
            }
           
            return customerCars;
        }

    }
}
