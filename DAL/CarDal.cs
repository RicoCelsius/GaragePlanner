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
        private readonly DbConnection _dbConnection;
        public CarDal(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void InsertCar(string email, Car car)
        {
            var getBrandIdQuery = "SELECT id FROM car_brand WHERE name = @brand";
            var brandIdParameter = new MySqlParameter("@brand", MySqlDbType.VarChar) { Value = car.Brand };
            var connection = _dbConnection;
            DataTable brandIdTable = connection.ExecuteQuery(getBrandIdQuery, new[] { brandIdParameter });

            if (brandIdTable.Rows.Count == 0)
            {
                // Handle case when brand ID is not found
                return;
            }

            int brandId = (int)brandIdTable.Rows[0]["id"];

            var insertCarQuery = "INSERT INTO car (customer_id, license_plate, brand_id, color, year) " +
                                 "SELECT id, @license_plate, @brand_id, @color, @year " +
                                 "FROM customers WHERE Email = @Email";

            MySqlParameter[] insertCarParameters =
            {
                new MySqlParameter("@Email", MySqlDbType.VarChar) { Value = email },
                new MySqlParameter("@license_plate", MySqlDbType.VarChar) { Value = car.LicensePlate },
                new MySqlParameter("@brand_id", MySqlDbType.Int32) { Value = brandId },
                new MySqlParameter("@color", MySqlDbType.VarChar) { Value = car.Color },
                new MySqlParameter("@year", MySqlDbType.Int32) { Value = car.Year },
            };

            connection.ExecuteNonQuery(insertCarQuery, insertCarParameters);
        }


        public void DeleteCar(int id)
        {
            var query = "DELETE FROM car WHERE id = @id";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", id)
            };
            var connection = _dbConnection;
            connection.ExecuteNonQuery(query, parameters);
        }

        public void UpdateCar(Car car)
        {
            var query = @"
        UPDATE car
        SET license_plate = @license_plate, brand_id = (
            SELECT id FROM car_brand WHERE name = @brand
        ), color = @color, year = @year
        WHERE id = @id";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", car.Id),
                new MySqlParameter("@license_plate", car.LicensePlate),
                new MySqlParameter("@brand", car.Brand),
                new MySqlParameter("@color", car.Color),
                new MySqlParameter("@year", car.Year)
            };

            var connection = _dbConnection;
            connection.ExecuteNonQuery(query, parameters);
        }




        public List<CarDto> GetCarsByEmail(string email)
        {
            var query = "SELECT car.*, car_brand.name AS brand_name FROM car " +
                        "INNER JOIN customers ON car.customer_id = customers.id " +
                        "INNER JOIN car_brand ON car.brand_id = car_brand.id " +
                        "WHERE customers.email = @Email";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", email)
            };
            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query, parameters);
            var cars = new List<CarDto>();
            foreach (DataRow row in dataTable.Rows)
            {
                var car = new CarDto(
                    row.Field<int>("id"),
                    row.Field<string>("license_plate"),
                    (Enums.Color)Enum.Parse(typeof(Enums.Color), row.Field<string>("color")),
                    row.Field<string>("brand_name"),
                    row.Field<int>("year")
                );
                cars.Add(car);
            }
            return cars;
        }


        public CarDto GetCarById(int id)
        {
            var query = "SELECT car.*, car_brand.name AS brand_name FROM car " +
                        "INNER JOIN car_brand ON car.brand_id = car_brand.id " +
                        "WHERE car.id = @id";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@id", id)
            };
            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count == 0)
            {
                throw new Exception("Car not found");
            }
            var row = dataTable.Rows[0];
            var carDto = new CarDto(
                row.Field<int>("id"),
                row.Field<string>("license_plate"),
                (Enums.Color)Enum.Parse(typeof(Enums.Color), row.Field<string>("color")),
                row.Field<string>("brand_name"),
                row.Field<int>("year")
            );
            return carDto;
        }


        public bool DoesCarAlreadyExist(string licensePlate)
        {
            var query = "SELECT * FROM car WHERE license_plate = @license_plate";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@license_plate", licensePlate)
            };
            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        public List<string> GetBrands()
        {
            var query = "SELECT * FROM car_brand";
            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query,null);
            var brands = new List<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                brands.Add(row.Field<string>("name"));
            }
            return brands;
        }

        public void InsertBrand(string brand)
        {
            var query = "INSERT INTO car_brand (name) VALUES (@name)";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name", brand)
            };
            var connection = _dbConnection;
            connection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteBrand(string brand)
        {
            var query = "DELETE FROM car_brand WHERE name = @name";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@name", brand)
            };
            var connection = _dbConnection;
            connection.ExecuteNonQuery(query, parameters);
        }

    }
}
