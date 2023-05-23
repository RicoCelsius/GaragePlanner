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

        public void CreateCar(int? customerId, Car car)
        {
            //_iCarDal.GetCarByLicensePlate(car.LicensePlate); // check if car already exists in db.
            _iCarDal.InsertCar(customerId,car);
        }


        public List<CarDto> GetCustomerCarsByCustomerEmail(string email)
        {
            List<CarDto> customerCars = _iCarDal.GetCarsByEmail(email);
           
            return customerCars;
        }

    }
}
