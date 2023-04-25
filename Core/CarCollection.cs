using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain.interfaces;
using MySqlConnector;

namespace Domain
{
    public class CarCollection
    {
        private List<Car> _cars;
        private ICarDal _iCarDal;

        public CarCollection(ICarDal iCarDal)
        {
            _cars = new List<Car>();
            _iCarDal = iCarDal;
        }

        public void CreateCar(Car car)
        {
            
            _iCarDal.GetCarByLicensePlate(car.LicensePlate); // check if car already exists in db.
            _iCarDal.InsertCar(car);
        }
    }
}
