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
    public class AppointmentCollection
    {
        public List<Day> Days { get;}
        private readonly IAppointmentDal _appointmentDal;
        private const int AmountOfDays = 14;

        public AppointmentCollection(IAppointmentDal appointmentDal)
        {
            Days = DayGenerator.GenerateDays(AmountOfDays);
            
            
            _appointmentDal = appointmentDal;
            LoadAgenda();
        }

        public void LoadAgenda()
        {
            foreach (Day day in Days)
            {
                List<AppointmentDto> appointments = _appointmentDal.GetAgendaOfDay(day.DateOfDay);

                foreach (AppointmentDto appointment in appointments)
                {
                    if (IsAppointmentDateAlreadyPassed(appointment))
                    {
                        continue;
                    }

                    Appointment appointmentToAdd = DtoConverter.ConvertAppointmentDtoToAppointment(appointment);

                    DateOnly appointmentDate = DateOnly.FromDateTime(appointment.DateAndTime);
                    TimeOnly appointmentTime = TimeOnly.FromDateTime(appointment.DateAndTime);

                    Day targetDay = Days.FirstOrDefault(d => d.DateOfDay.Equals(appointmentDate));

                    if (targetDay != null)
                    {
                        TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointmentTime);

                        if (targetTimeSlot != null)
                        {
                            targetTimeSlot.AddAppointment(appointmentToAdd);
                        }
                    }
                }
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
            if (targetTimeSlot.IsAvailable())
            {
                AppointmentDto appointmentDto = DomainConverter.ConvertAppointmentToAppointmentDto(appointment);
                _appointmentDal.InsertAppointment(appointmentDto);
                return new Result(true, "Appointment created");
            }
            return new Result(false, "Appointment could not be created");

            




        }

    }
}
