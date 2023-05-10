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
    public class AgendaDal : IAgendaDal
    {

        public Agenda GetAgenda()
        {
            Agenda agenda = new Agenda();
            List<Appointment> appointments = new List<Appointment>();
            List<DateTime> dates = new List<DateTime>();

            var query = @"
        SELECT appointment.date, appointment.type, appointment.status, 
            customer.first_name, customer.last_name, customer.address, 
            car.make, car.model, car.year 
        FROM appointment 
        INNER JOIN customer ON appointment.customer_id = customer.id 
        INNER JOIN car ON appointment.car_id = car.id";

            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, null);

            foreach (DataRow row in dataTable.Rows)
            {
                Customer customer = new Customer(
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("address"),
                    row.Field<string>("email"),
                    row.Field<string>("password")
                );

                Car car = new Car(
                    row.Field<string>("license_plate"),
                    row.Field<string>("red"),
                    row.Field<string>("model"),
                    row.Field<int>("year")
                );

                Appointment appointment = new Appointment(
                    row.Field<Enums.Type>("type"),
                    row.Field<Enums.Status>("status"),
                    customer,
                    car
                );
               
                dates.Add(row.Field<DateTime>("date"));
                appointments.Add(appointment);
            }

            agenda.LoadAgenda(dates, appointments);

            return agenda;
        }


    }
}
