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
        public bool AppointmentExistsByDateAndTime(DateTime dateAndTime)
        {
            var mysqlDateTime = dateAndTime.ToString("yyyy-MM-dd HH:mm:ss");
            var query = "SELECT EXISTS(SELECT 1 FROM appointment WHERE date = @dateAndTime)";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@dateAndTime", MySqlDbType.DateTime) { Value = dateAndTime },
            };
            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);
            var result = Convert.ToBoolean(dataTable.Rows[0][0]);

            return Convert.ToBoolean(result);
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

            var connection = new DbConnection();
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

        public void InsertAppointment(int? id, AppointmentDto appointment)
        {
            var query = "INSERT INTO appointment (customer_id, car_id, date, type, status) " +
                        "VALUES (@customer_id, @car_id, @date, @type, @status)";

            var connection = new DbConnection();

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@customer_id", MySqlDbType.Int32) { Value = id },
                new MySqlParameter("@car_id", MySqlDbType.Int32) { Value = appointment.Car.Id },
                new MySqlParameter("@date", MySqlDbType.DateTime) { Value = appointment.Date },
                new MySqlParameter("@type", MySqlDbType.VarChar, 50) { Value = appointment.ServiceType },
                new MySqlParameter("@status", MySqlDbType.VarChar, 50) { Value = appointment.Status }
            };

            connection.ExecuteQuery(query, parameters);
        }
    }
}
