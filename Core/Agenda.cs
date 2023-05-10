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
            GenerateDays(14);
            _appointmentDal = appointmentDal;
        }

        public Agenda()
        {
            Days = new List<Day>();
            GenerateDays(14);
        }




        public bool TryCreateAppointment(DateTime appointmentDateTime, Enums.Type type, Enums.Status status, Customer customer, Car car)
        {
            DateOnly appointmentDate = DateOnly.FromDateTime(appointmentDateTime);
            TimeOnly appointmentTime = TimeOnly.FromDateTime(appointmentDateTime);

            Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));


            TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);

            Appointment appointment = new(type, status, customer, car);

            if (targetTimeSlot.TryAddAppointment(appointment))
            {
                _appointmentDal.InsertAppointment(appointment);
                return true;
            }
            return false;
        }

        public void loadAgenda(DateTime date, List<Appointment> appointments)
        {
            foreach (Appointment appointment in appointments)
            {
                TryCreateAppointment(date, appointment.ServiceType, appointment.Status, appointment.Customer, appointment.Car);
            }
        }







        private void GenerateDays(int amountOfDays)
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
