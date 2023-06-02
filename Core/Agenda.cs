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
        private const int AmountOfDays = 14;

        public Agenda(IAppointmentDal appointmentDal)
        {
            Days = DayGenerator.GenerateDays(AmountOfDays);
            
            
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

                DateOnly appointmentDate = DateOnly.FromDateTime(appointment.DateAndTime);
                TimeOnly appointmentTime = TimeOnly.FromDateTime(appointment.DateAndTime);

                Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));

                TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);
                targetTimeSlot.AddAppointment(appointmentToAdd);


            }
        }

        private bool IsAppointmentDateAlreadyPassed(AppointmentDto appointment)
        {
            return appointment.DateAndTime < DateTime.Now;
        }

        public Result CreateAppointment(Appointment appointment)
        {
            DateOnly appointmentDate = DateOnly.FromDateTime(appointment.DateAndTime);
            TimeOnly appointmentTime = TimeOnly.FromDateTime(appointment.DateAndTime);

            Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointmentDate));

            TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);
            

            AppointmentDto appointmentDto = DomainConverter.ConvertAppointmentToAppointmentDto(appointment);
            _appointmentDal.InsertAppointment(appointmentDto);
            return targetTimeSlot.AddAppointment(appointment);



        }

    }
}
