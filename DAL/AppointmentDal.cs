using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Domain.interfaces;
using MySqlConnector;
using Domain;

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




        public void InsertAppointment(Appointment appointment)
        {
            var query = "INSERT INTO appointment (date, type, status) " +
                        "VALUES (@customer_id, @vehicle_id, @date, @type, @status)";
            var connection = new DbConnection();
            MySqlParameter[] parameters =
            {

                new("@type", MySqlDbType.VarChar, 50) { Value = appointment.ServiceType},
                new("@status", MySqlDbType.VarChar, 50) { Value = appointment.Status }
            };  
            connection.ExecuteQuery(query, parameters);


        }
    }
}