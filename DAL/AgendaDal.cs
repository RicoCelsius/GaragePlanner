using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MySqlConnector;

namespace DAL
{
    public class AgendaDal
    {
        public Agenda getAgenda()
        {
            Agenda agenda = new Agenda();
            AppointmentDal appointmentDal = new AppointmentDal();
            List<Appointment> appointments = new();




            var query = "SELECT * FROM appointment";


            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, null);

            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                agenda.loadAgenda(row.Field<DateTime>("date"));
            }

            appointments.Add(new Appointment());
            
          

            return agenda;
        }
    }
}
