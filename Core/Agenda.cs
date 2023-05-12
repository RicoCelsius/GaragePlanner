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
        public List<Day> Days { get; set; }
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
                Appointment appointmentToAdd = DtoConverter.ConvertAppointmentDtoToAppointment(appointment);

                DateOnly appointmentDate = DateOnly.FromDateTime(appointment.Date);
                TimeOnly appointmentTime = TimeOnly.FromDateTime(appointment.Date);

                Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));

                TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);

                targetTimeSlot.TryAddAppointment(appointmentToAdd);
            }
        }

        public bool CreateAppointment(AppointmentDto appointmentDto, CustomerDto customer)
        {
            DateOnly appointmentDate = DateOnly.FromDateTime(appointmentDto.Date);
            TimeOnly appointmentTime = TimeOnly.FromDateTime(appointmentDto.Date);
            Appointment appointment = DtoConverter.ConvertAppointmentDtoToAppointment(appointmentDto);

            Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));

            TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);

            if (targetTimeSlot.TryAddAppointment(appointment))
            {
                _appointmentDal.InsertAppointment(customer.Id, appointmentDto);
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
