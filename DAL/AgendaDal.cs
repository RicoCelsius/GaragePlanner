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
            customers.first_name, customers.last_name, customers.address, customers.email, customers.password, 
            car.license_plate, car.color, car.model, car.year 
        FROM appointment 
        INNER JOIN customers ON appointment.customer_id = customers.id 
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
                    row.Field<string>("color"),
                    row.Field<string>("model"),
                    row.Field<int>("year")
                );

                Appointment appointment = new Appointment(
                    (Enums.Type)Enum.Parse(typeof(Enums.Type), row.Field<string>("type")),
                    (Enums.Status)Enum.Parse(typeof(Enums.Status), row.Field<string>("status")),
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
