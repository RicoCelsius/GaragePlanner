using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.interfaces;
using MySqlConnector;

namespace DAL
{
    public class CarDal : ICarDal
    {
        public void InsertCar(Car car)
        {
            var query = "INSERT INTO cars (license_plate, color, model, year) " +
                        "VALUES (@license_plate, @color, @model, @year)";
            MySqlParameter[] parameters =
            {
                new("@license_plate", MySqlDbType.VarChar, 50) { Value = car.LicensePlate },
                new("@color", MySqlDbType.VarChar, 50) { Value = car.Color },
                new("@model", MySqlDbType.VarChar, 100) { Value = car.Model },
                new("@year", MySqlDbType.VarChar, 50) { Value = car.Year }
            };
            var connection = new DbConnection();
            connection.ExecuteQuery(query, parameters);
        }


        public List<Car> GetCarsByCustomerId(int customerId)
        {
            var query = "SELECT * FROM cars WHERE customer_id = @customer_id";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@customer_id", customerId)
            };
            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);
            var cars = new List<Car>();
            foreach (DataRow row in dataTable.Rows)
            {
                var car = new Car(
                    row.Field<string>("license_plate"),
                    row.Field<string>("color"),
                    row.Field<string>("model"),
                    row.Field<int>("year")
                );
                cars.Add(car);
            }
            return cars;
        }


        public Car GetCarByLicensePlate(string licensePlate)
        {
            var query = "SELECT * FROM cars WHERE license_plate = @license_plate";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@license_plate", licensePlate)
            };
            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count == 0)
            {
                throw new Exception("Car not found");
            }
            var row = dataTable.Rows[0];
            var car = new Car(
                row.Field<string>("license_plate"),
                row.Field<string>("color"),
                row.Field<string>("model"),
                row.Field<int>("year")
            );
            return car;
        }






    }
}
