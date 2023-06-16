using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Domain.interfaces;
using MySqlConnector;
using Domain;
using Domain.dto;

namespace DAL
{
    public class AppointmentDal : IAppointmentDal
    {
        private readonly DbConnection _dbConnection;
        public AppointmentDal(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<AppointmentDto> GetAgendaOfDay(DateOnly date)
        {
            List<AppointmentDto> appointments = new();
            string mySqlDate = date.ToString("yyyy-MM-dd");

            var query = @"
         SELECT appointment.id, appointment.date, appointment.type, appointment.status, appointment.time, 
        customers.id, customers.first_name, customers.last_name, customers.Address, customers.Email, customers.Password, car.id,
        car.license_plate, car.color, car.model, car.year 
        FROM appointment 
        INNER JOIN customers ON appointment.customer_id = customers.id 
        INNER JOIN car ON appointment.car_id = car.id
        WHERE appointment.date = @mySqlDate";


            var connection = _dbConnection;
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@mySqlDate", MySqlDbType.Date) { Value = date }
            };

            var dataTable = connection.ExecuteQuery(query, parameters);


            foreach (DataRow row in dataTable.Rows)
            {
                CustomerDto customer = new(
                    row.Field<int>("id"),
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("Address"),
                    row.Field<string>("Email"),
                    row.Field<string>("Password")
                );

                CarDto car = new(
                    row.Field<int>("id"),
                    row.Field<string>("license_plate"),
                    (Enums.Color)Enum.Parse(typeof(Enums.Color), row.Field<string>("color")),
                    row.Field<string>("model"),
                    row.Field<int>("year")
                );

                AppointmentDto appointment = new(
                    row.Field<int>("id"),
                    DateOnly.FromDateTime(row.Field<DateTime>("date")),
                    TimeOnly.FromTimeSpan(row.Field<TimeSpan>("time")),
                    (Enums.Type)Enum.Parse(typeof(Enums.Type), row.Field<string>("type")),
                    (Enums.Status)Enum.Parse(typeof(Enums.Status), row.Field<string>("status")),
                    customer,
                    car
                );

                appointments.Add(appointment);
            }

            return appointments;
        }


        public List<AppointmentDto> GetAgenda()
        {
            List<AppointmentDto> appointments = new ();

            var query = @"
                SELECT appointment.id, appointment.date, appointment.type, appointment.status, 
                    customers.id, customers.first_name, customers.last_name, customers.Address, customers.Email, customers.Password, car.id,
                    car.license_plate, car.color, car.model, car.year 
                FROM appointment 
                INNER JOIN customers ON appointment.customer_id = customers.id 
                INNER JOIN car ON appointment.car_id = car.id";

            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query, null);

            foreach (DataRow row in dataTable.Rows)
            {
                CustomerDto customer = new(
                    row.Field<int>("id"),
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("Address"),
                    row.Field<string>("Email"),
                    row.Field<string>("Password")
                );

                CarDto car = new(
                    row.Field<int>("id"),
                    row.Field<string>("license_plate"),
                    (Enums.Color)Enum.Parse(typeof(Enums.Color), row.Field<string>("color")),
                    row.Field<string>("model"),
                    row.Field<int>("year")
                );

                AppointmentDto appointment = new (
                    row.Field<int>("id"),
                    row.Field<DateOnly>("date"),
                    row.Field<TimeOnly>("time"),
                    (Enums.Type)Enum.Parse(typeof(Enums.Type), 
                        row.Field<string>("type")),
                    (Enums.Status)Enum.Parse(typeof(Enums.Status), row.Field<string>("status")),
                    customer,
                    car
                );

                appointments.Add(appointment);
            }

            return appointments;
        }

        public void InsertAppointment(Appointment appointment)
        {
            var query = @"
         INSERT INTO appointment (customer_id, car_id, date, time, type, status) 
         VALUES (
        (SELECT id FROM customers WHERE email = @Email),
        (SELECT id FROM car WHERE license_plate = @LicensePlate),
        @Date,
        @Time, 
        @Type, 
        @Status)";

            var connection = _dbConnection;

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Email", MySqlDbType.VarChar) { Value = appointment.Customer.Email },
                new MySqlParameter("@LicensePlate", MySqlDbType.VarChar) { Value = appointment.Car.LicensePlate },
                new MySqlParameter("@Date", MySqlDbType.Date) { Value = appointment.Date },
                new MySqlParameter("@Time", MySqlDbType.Time) { Value = appointment.Time },
                new MySqlParameter("@Type", MySqlDbType.VarChar) { Value = appointment.ServiceType },
                new MySqlParameter("@Status", MySqlDbType.VarChar) { Value = appointment.Status }
            };

            connection.ExecuteNonQuery(query, parameters);
        }


    }
}
