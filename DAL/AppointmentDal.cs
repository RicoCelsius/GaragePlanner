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


        public List<AppointmentDto> GetAgenda()
        {
            List<AppointmentDto> appointments = new List<AppointmentDto>();

            var query = @"
                SELECT appointment.date, appointment.type, appointment.status, 
                    customers.id, customers.first_name, customers.last_name, customers.Address, customers.Email, customers.Password, car.id,
                    car.license_plate, car.color, car.model, car.year 
                FROM appointment 
                INNER JOIN customers ON appointment.customer_id = customers.id 
                INNER JOIN car ON appointment.car_id = car.id";

            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query, null);

            foreach (DataRow row in dataTable.Rows)
            {
                CustomerDto customer = new CustomerDto(
                    row.Field<int>("id"),
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("Address"),
                    row.Field<string>("Email"),
                    row.Field<string>("Password")
                );

                CarDto car = new CarDto(
                    row.Field<int>("id"),
                    row.Field<string>("license_plate"),
                    row.Field<string>("color"),
                    row.Field<string>("model"),
                    row.Field<int>("year")
                );

                AppointmentDto appointment = new AppointmentDto(
                    row.Field<DateTime>("date"),
                    (Enums.Type)Enum.Parse(typeof(Enums.Type), row.Field<string>("type")),
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
             INSERT INTO appointment (customer_id, car_id, date, type, status) 
             VALUES (
            (SELECT id FROM customers WHERE email = @Email),
            (SELECT id FROM car WHERE license_plate = @LicensePlate),
            @Date, 
            @Type, 
            @Status)";

            var connection = _dbConnection;

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Email", MySqlDbType.VarChar) { Value = appointment.Customer.Email },
                new MySqlParameter("@LicensePlate", MySqlDbType.VarChar) { Value = appointment.Car.LicensePlate },
                new MySqlParameter("@Date", MySqlDbType.DateTime) { Value = appointment.DateAndTime },
                new MySqlParameter("@Type", MySqlDbType.VarChar) { Value = appointment.ServiceType },
                new MySqlParameter("@Status", MySqlDbType.VarChar) { Value = appointment.Status }
            };

            connection.ExecuteQuery(query, parameters);
        }

    }
}
