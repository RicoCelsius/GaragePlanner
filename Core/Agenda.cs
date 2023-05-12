using Domain.interfaces;
using Domain.utils;
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
            this.LoadAgenda(_appointmentDal.GetAgenda());
        }


        public void LoadAgenda(List<AppointmentDto> appointments)
        {
            foreach (AppointmentDto appointment in appointments)
            {
                Customer customer = DtoConverter.ConvertCustomerDtoToCustomer(appointment.Customer);
                Car car = DtoConverter.ConvertCarDtoToCar(appointment.Car);
                this.TryCreateAppointment(appointment.Date, appointment.ServiceType, appointment.Status, customer, car,false);
            }
        }


        public bool TryCreateAppointment(DateTime appointmentDateTime, Enums.Type type, Enums.Status status, Customer customer, Car car, bool updateDb = true)
        {
            DateOnly appointmentDate = DateOnly.FromDateTime(appointmentDateTime);
            TimeOnly appointmentTime = TimeOnly.FromDateTime(appointmentDateTime);

            Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));

            TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);

            Appointment appointment = new(type, status, customer, car);

            if (targetTimeSlot.TryAddAppointment(appointment))
            {
                if (updateDb)
                {
                    _appointmentDal.InsertAppointment(appointment);
                }
                return true;
            }
            return false;
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
