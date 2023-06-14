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
        public IReadOnlyList<Day> Days { get;}
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
                List<AppointmentDto> appointments;

                try
                {
                    appointments = _appointmentDal.GetAgendaOfDay(day.DateOfDay);
                }

                catch (Exception e)
                {
                    throw new CouldNotReadDataException("Could not read data", e);
                }

                foreach (AppointmentDto appointment in appointments)
                {
                    Appointment appointmentToAdd = DtoConverter.ConvertAppointmentDtoToAppointment(appointment);

                    DateOnly appointmentDate = appointment.Date;
                    TimeOnly appointmentTime = appointment.Time;

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



        public Result CreateAppointment(Appointment appointment)
        {


            Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointment.Date));

            TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointment.Time);
            if (targetTimeSlot.IsAvailable())
            {
                AppointmentDto appointmentDto = DomainConverter.ConvertAppointmentToAppointmentDto(appointment);

                try
                {
                    _appointmentDal.InsertAppointment(appointmentDto);
                }
                catch (Exception e)
                {
                    throw new CouldNotInsertDataException("Appointment could not be inserted", e);
                }

                return new Result(true, "Appointment created");
            }
            return new Result(false, "Appointment could not be created");


        }

    }
}
