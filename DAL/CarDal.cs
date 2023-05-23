using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using Domain.interfaces;
using MySqlConnector;

namespace DAL
{
    public class CarDal : ICarDal
    {
        public void InsertCar(int? customerId, Car car)
        {
            var query = "INSERT INTO car (customer_id, license_plate, model, color, year) " +
                        "VALUES (@customer_id, @license_plate, @model, @color, @year)";
            MySqlParameter[] parameters =
            {
                new("@customer_id", MySqlDbType.VarChar) { Value = customerId },
                new("@license_plate", MySqlDbType.VarChar) { Value = car.LicensePlate },
                new("@model", MySqlDbType.VarChar) { Value = car.Model },
                new("@color", MySqlDbType.VarChar) { Value = car.Color },
                new("@year", MySqlDbType.VarChar) { Value = car.Year }
            };
            var connection = new DbConnection();
            connection.ExecuteQuery(query, parameters);
        }

        public List<CarDto> GetCarsByEmail(string email)
        {
            var query = "SELECT car.* FROM car " +
                        "INNER JOIN customers ON car.customer_id = customers.id " + // Corrected this line
                        "WHERE customers.email = @Email"; // It's good to specify the table name here too
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", email)
            };
            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);
            var cars = new List<CarDto>();
            foreach (DataRow row in dataTable.Rows)
            {
                var car = new CarDto(
                    row.Field<int>("id"),
                    row.Field<string>("license_plate"),
                    row.Field<string>("color"),
                    row.Field<string>("model"),
                    row.Field<int>("year")
                );
                cars.Add(car);
            }
            return cars;
        }


        public CarDto GetCarByLicensePlate(string licensePlate)
        {
            var query = "SELECT * FROM car WHERE license_plate = @license_plate";
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
            var carDto = new CarDto(
                row.Field<int>("id"),
                row.Field<string>("license_plate"),
                row.Field<string>("color"),
                row.Field<string>("model"),
                row.Field<int>("year")
            );
            return carDto;
        }
    }
}
