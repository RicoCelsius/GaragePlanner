using System.Collections.Generic;
using System.Linq;
using DAL.dto;
using Domain.interfaces;

namespace Domain
{
    public class AppointmentCollection
    {
        private readonly IAppointmentDal _appointmentDal;

        public AppointmentCollection(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }



        private DateTime GetCurrentDate()
        {
            return DateTime.Today;
        }
        
        private List<DateTime> GetDatesTwoWeeksFromNow()
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime currentDate = GetCurrentDate();
            for (int i = 0; i < 14; i++)
            {
                dates.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }

            return dates;
        }
        
        public List<DateTime> GetAvailableDates()
        {
            List<DateTime> availableDates = GetDatesTwoWeeksFromNow();
            foreach (var appointment in appointments)
            {
                availableDates.Remove(appointment.Date);
            }

            return availableDates;
        }
        
        
        
        


        public void AddAppointment(AppointmentDto appointment)
        {
            _appointmentDal.Create(appointment);
        }

        public void RemoveAppointment(AppointmentDto appointment)
        {
            _appointmentDal.Delete(appointment.Id);
        }
        
        

        /*public List<Appointment> GetAppointments()
        {
            throw Exception("Not implemented");
        }*/
    }
}