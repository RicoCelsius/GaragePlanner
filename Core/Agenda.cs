using Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Agenda
    {
        public List<Day> Days { get; set; }

        public Agenda()
        {
            Days = new List<Day>();
            GenerateTwoWeeksDays();
        }

        public bool TryCreateAppointment(DateTime appointmentDateTime, Enums.Type type, Enums.Status status, Customer customer, Car car)
        {
            DateOnly appointmentDate = DateOnly.FromDateTime(appointmentDateTime);
            TimeOnly appointmentTime = TimeOnly.FromDateTime(appointmentDateTime);

            TimeSlot targetTimeSlot = Days.Find(day => day.Date == appointmentDate).FindTimeSlot(appointmentTime);

            Appointment appointment = new(type, status, customer, car);

            if (targetTimeSlot.TryAddAppointment(appointment))
            {
                return true;
            }

            return false;
            

          
           

            return targetTimeSlot.TryAddAppointment(appointment);
        }



        private void GenerateTwoWeeksDays()
        {
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
            for (int i = 0; i < 14; i++)
            {
                Days.Add(new Day(startDate));
                startDate = startDate.AddDays(1);
            }
        }

    }
}
