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
        public List<Car> Cars { get; set; }
        private ICarDal _iCarDal;

        public CarCollection(ICarDal iCarDal)
        {
            _iCarDal = iCarDal;
        }

        public void CreateCar(string email, Car car)
        {
            //_iCarDal.GetCarByLicensePlate(car.LicensePlate); // check if car already exists in db.
            _iCarDal.InsertCar(email,car);
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


       /* public bool doesLicensePlateAlreadyExist(string licenseplate)
        {
          

        }*/

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
