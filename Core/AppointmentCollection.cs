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
        
        public List<DateTime> GetDatesTwoWeeksFromNow()
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

        public void TryCreateAppointment(DateTime appointmentDate)
        {
            Appointment appointment = new Appointment(appointmentDate,Enums.Type.OilChange,Enums.Status.Scheduled,1,1,"09:00");
            _appointmentDal.InsertAppointment(appointment);
        }

        public List<DateTime> GetAvailableDateAndTimeSlots()
        {
            List<DateTime> datesAndTimeSlots = GenerateDatesAndTimeSlots();
            List<DateTime> availableDates = new List<DateTime>();
            foreach (DateTime dateAndTime in datesAndTimeSlots)
            {
                if (_appointmentDal.GetAppointmentByDateAndTime(dateAndTime) == null)
                {
                    availableDates.Add(dateAndTime);
                }
            }
            return availableDates;
        }



        public List<DateTime> GenerateDatesAndTimeSlots()
        {
            var currentDate = DateTime.Today;
            var datesTwoWeeksFromNow = Enumerable.Range(0, 14)
                .Select(i => currentDate.AddDays(i))
                .ToList();

            var timeSlots = new List<string>
            {
                "09:00", "10:00", "11:00", "12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00"
            };

            var datesAndTimeSlots = new List<DateTime>();
            foreach (var date in datesTwoWeeksFromNow)
            {
                foreach (var time in timeSlots)
                {
                    var dateTime = date.Add(TimeSpan.Parse(time));
                    datesAndTimeSlots.Add(dateTime);
                }
            }

            return datesAndTimeSlots;
        }













        /*public List<Appointment> GetAppointments()
        {
            throw Exception("Not implemented");
        }*/
    }
}