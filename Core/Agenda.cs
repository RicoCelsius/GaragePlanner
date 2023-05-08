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

        public bool TryCreateAppointment(DateTime appointmentDateTime,Enums.Type type,Enums.Status status, Customer customer, Car car)
        {

            var targetDay = Days.Find(day => day.Date.Date == appointmentDateTime.Date);

            if (targetDay == null || !targetDay.IsDayAvailable())
            {
                return false;
            }

            var targetTimeSlot = targetDay.TimeSlots.Find(timeSlot => timeSlot.StartTime == appointmentDateTime);

            if (targetTimeSlot == null || !targetTimeSlot.IsAvailable())
            {
                return false;
            }

            Appointment appointment = new Appointment(appointmentDateTime,type,status,customer,car);


            return targetTimeSlot.tryAddAppointment(appointment);
        }
        

        private void GenerateTwoWeeksDays()
        {
            DateTime startDate = DateTime.Now.Date.AddDays(1);
            for (int i = 0; i < 14; i++)
            {
                Days.Add(new Day(startDate));
                startDate = startDate.AddDays(1);
            }
        }

    }
}
