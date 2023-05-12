﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain.dto;
using Domain.interfaces;
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

        public void CreateCar(Car car)
        {
            _iCarDal.GetCarByLicensePlate(car.LicensePlate); // check if car already exists in db.
            _iCarDal.InsertCar(car);
        }


        public List<CarDto> GetCustomerCars(int id)
        {
            List<CarDto> customerCars = _iCarDal.GetCarsByCustomerId(id);
            return customerCars;
        }

    }
}
