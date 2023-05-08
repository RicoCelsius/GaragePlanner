using Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class AppointmentValidator
    {
        private static readonly IAppointmentDal _appointmentDal;
        private static readonly List<DateTime> _availableDatesAndTimeSlots = new List<DateTime>();




        public static bool IsDateTimeValid(DateTime dateAndTime)
        {
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);
            List<DateTime> availableDateTimes = GetAvailableDateAndTimeSlots();
            return availableDateTimes.Contains(dateAndTime);
        }

        public static List<DateTime> GetAvailableDateAndTimeSlots()
        {
            List<DateTime> datesAndTimeSlots = GenerateDatesAndTimeSlots();

            foreach (DateTime dateAndTime in datesAndTimeSlots)
            {
                if (!_appointmentDal.AppointmentExistsByDateAndTime(dateAndTime))
                {
                    _availableDatesAndTimeSlots.Add(dateAndTime);
                }
            }
            return _availableDatesAndTimeSlots;
        }



        public static List<DateTime> GenerateDatesAndTimeSlots()
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


    }
}
