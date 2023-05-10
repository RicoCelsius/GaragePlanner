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
        private readonly IAppointmentDal _appointmentDal;

        public Agenda(IAppointmentDal appointmentDal)
        {
            Days = new List<Day>();
            GenerateTwoWeeksDays();
            _appointmentDal = appointmentDal;
        }

        public bool TryCreateAppointment(DateTime appointmentDateTime, Enums.Type type, Enums.Status status, Customer customer, Car car)
        {
            DateOnly appointmentDate = DateOnly.FromDateTime(appointmentDateTime);
            TimeOnly appointmentTime = TimeOnly.FromDateTime(appointmentDateTime);

            TimeSlot targetTimeSlot = Days.Find(day => day.Date == appointmentDate).FindTimeSlot(appointmentTime);

            Appointment appointment = new(type, status, customer, car);

            if (targetTimeSlot.TryAddAppointment(appointment))
            {
                _appointmentDal.InsertAppointment(appointment);
                return true;
            }
            return false;

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
