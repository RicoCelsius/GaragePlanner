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
                    throw new DalException("Could not read data", e);
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



        public bool TryCreateAppointment(DateOnly date, TimeOnly time,Enums.Type serviceType,Customer customer,Car car)
        {
            LoadAgenda();
            Appointment appointment = new Appointment(date,time,serviceType,Enums.Status.Scheduled,customer,car);

            Day targetDay = Days.FirstOrDefault(day => day.DateOfDay.Equals(appointment.Date));

            TimeSlot targetTimeSlot = targetDay.FindTimeSlot(appointment.Time);
            if (targetTimeSlot.IsAvailable())
            {

                try
                {
                    _appointmentDal.InsertAppointment(appointment);
                }
                catch (Exception e)
                {
                    throw new DalException("Appointment could not be inserted", e);
                }

                return true;
            }

            return false;


        }

    }
}
