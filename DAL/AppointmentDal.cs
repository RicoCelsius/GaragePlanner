using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using DAL.dto;
using Domain.interfaces;
using MySqlConnector;
using Domain;

namespace DAL
{
    public class AppointmentDal : IAppointmentDal
    {
        public AppointmentDto? GetAppointmentByDate(DateTime date)
        {
            var query = "SELECT * FROM appointment WHERE date = @date";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@date", date)
            };
            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }

            var row = dataTable.Rows[0];
            var appointment = new AppointmentDto(
                row.Field<int>("id"),
                row.Field<int>("customer_id"),
                row.Field<int>("car_id"),
                row.Field<DateTime>("date"),
                row.Field<int>("type"),
                row.Field<int>("status")
            );
            return appointment;
           
        }

        public void InsertAppointment(Appointment appointment)
        {
            var query = "INSERT INTO appointment (customer_id, car_id, date, type, status) " +
                        "VALUES (@customer_id, @vehicle_id, @date, @type, @status)";
            var connection = new DbConnection();
            MySqlParameter[] parameters =
            {
                new("@customer_id", MySqlDbType.Int32) { Value = appointment.CustomerId },
                new("@vehicle_id", MySqlDbType.Int32) { Value = appointment.CarId },
                new("@date", MySqlDbType.DateTime) { Value = appointment.Date },
                new("@type", MySqlDbType.VarChar, 50) { Value = appointment.ServiceType},
                new("@status", MySqlDbType.VarChar, 50) { Value = appointment.Status }
            };  
            connection.ExecuteQuery(query, parameters);


        }
    }
}