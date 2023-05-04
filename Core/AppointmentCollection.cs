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

        private bool IsDateAvailable(DateTime date)
        {
            return _appointmentDal.GetAppointmentByDate(date) == null;
        }

        public void TryCreateAppointment(DateTime appointmentDate)
        {
            Appointment appointment = new Appointment(appointmentDate,Enums.Type.OilChange,Enums.Status.Scheduled,1,1);
            _appointmentDal.InsertAppointment(appointment);
        }

        public List<DateTime> GetAvailableDates()
        {
            List<DateTime> datesTwoWeeksFromNow = GetDatesTwoWeeksFromNow();
            List<DateTime> availableDates = new List<DateTime>();
            foreach (DateTime date in datesTwoWeeksFromNow)
            {
                if (_appointmentDal.GetAppointmentByDate(date) == null)
                {
                    availableDates.Add(date);
                }
            }
            return availableDates;
        }

        public List<string> GenerateTimeSlots(DateTime date)
        {
            List<string> timeSlots = new List<string>();
            DateTime startTime = date.Date.AddHours(9);

            while (startTime <= date.Date.AddHours(17))
            {
                timeSlots.Add(startTime.ToString("hh:mm tt"));
                startTime = startTime.AddMinutes(30);
            }

            return timeSlots;
        }



        public List<String> GetAvailableTimeSlots(DateTime selectedDate)
        {
            return GenerateTimeSlots(selectedDate);
        }











        /*public List<Appointment> GetAppointments()
        {
            throw Exception("Not implemented");
        }*/
    }
}