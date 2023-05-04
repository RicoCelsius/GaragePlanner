using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using DAL.dto;
using Domain.interfaces;
using MySqlConnector;

namespace DAL
{
    internal class AppointmentDal : IAppointmentDal
    {
        public void Create(AppointmentDto appointment)
        {
            throw new NotImplementedException();
        }



        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AppointmentDto GetAppointmentByDate(DateTime date)
        {
            var query = "SELECT * FROM appointments WHERE date = @date";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@date", date)
            };
            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);
            var appointment = new AppointmentDto();
            foreach (var row in dataTable.Rows)
            {
                appointment.Id = row.Field<int>("id");
                appointment.Date = row.Field<DateTime>("date");
                appointment.CustomerId = row.Field<int>("customer_id");
            }
            return appointment;
            

        }


    }
}
    

