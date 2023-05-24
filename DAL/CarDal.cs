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
        public void InsertCar(string email, Car car)
        {
            var insertCarQuery = "INSERT INTO car (customer_id, license_plate, model, color, year) " +
                                 "SELECT id, @license_plate, @model, @color, @year " +
                                 "FROM customers WHERE Email = @Email";

            MySqlParameter[] insertCarParameters =
            {
                new MySqlParameter("@Email", MySqlDbType.VarChar) { Value = email },
                new MySqlParameter("@license_plate", MySqlDbType.VarChar) { Value = car.LicensePlate },
                new MySqlParameter("@model", MySqlDbType.VarChar) { Value = car.Model },
                new MySqlParameter("@color", MySqlDbType.VarChar) { Value = car.Color },
                new MySqlParameter("@year", MySqlDbType.Int32) { Value = car.Year },
            };

            var connection = new DbConnection();
            connection.ExecuteQuery(insertCarQuery, insertCarParameters);
        }

        public void DeleteCar(int id)
        {
            var query = "DELETE FROM car WHERE id = @id";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", id)
            };
            var connection = new DbConnection();
            connection.ExecuteQuery(query, parameters);
        }

        public void UpdateCar(Car car)
        {
            var query = "UPDATE car SET license_plate = @license_plate, model = @model, color = @color, year = @year WHERE id = @id";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", car.Id),
                new MySqlParameter("@license_plate", car.LicensePlate),
                new MySqlParameter("@model", car.Model),
                new MySqlParameter("@color", car.Color),
                new MySqlParameter("@year", car.Year)
            };
            var connection = new DbConnection();
            connection.ExecuteQuery(query, parameters);
        }



        public List<CarDto> GetCarsByEmail(string email)
        {
            var query = "SELECT car.* FROM car " +
                        "INNER JOIN customers ON car.customer_id = customers.id " + 
                        "WHERE customers.email = @Email"; 
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
