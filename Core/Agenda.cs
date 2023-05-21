using Domain.interfaces;
using Domain.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.dto;

namespace Domain
{
    public class Agenda
    {
        public List<Day> Days { get;}
        private readonly IAppointmentDal _appointmentDal;

        public Agenda(IAppointmentDal appointmentDal)
        {
            Days = new List<Day>();
            GenerateDays(14);
            _appointmentDal = appointmentDal;
            LoadAgenda(_appointmentDal.GetAgenda());
        }

        public void LoadAgenda(List<AppointmentDto> appointments)
        {
            foreach (AppointmentDto appointment in appointments)
            {
                if (IsAppointmentDateAlreadyPassed(appointment))
                {
                    continue;
                }
                Appointment appointmentToAdd = DtoConverter.ConvertAppointmentDtoToAppointment(appointment);

                DateOnly appointmentDate = DateOnly.FromDateTime(appointment.Date);
                TimeOnly appointmentTime = TimeOnly.FromDateTime(appointment.Date);

                Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));

                TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);

                targetTimeSlot.TryAddAppointment(appointmentToAdd);
            }
        }

        private bool IsAppointmentDateAlreadyPassed(AppointmentDto appointment)
        {
            return appointment.Date < DateTime.Now;
        }

        public bool AddAppointment(Appointment appointment)
        {
            DateOnly appointmentDate = DateOnly.FromDateTime(appointment.DateAndTime);
            TimeOnly appointmentTime = TimeOnly.FromDateTime(appointment.DateAndTime);

            Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));

            TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);

            if (targetTimeSlot.TryAddAppointment(appointment))
            {
                _appointmentDal.InsertAppointment(appointment);
                return true;
            }
            return false;
        }

        private void GenerateDays(int amountOfDays)
        {
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
            for (int i = 0; i < amountOfDays; i++)
            {
                Days.Add(new Day(startDate));
                startDate = startDate.AddDays(1);
            }
        }
    }
}
