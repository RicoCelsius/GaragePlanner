using System.Collections.Generic;
using System.Linq;
using DAL.dto;
using Domain.interfaces;

namespace Domain
{
    public class Agenda
    {
        private readonly IAppointmentDal _appointmentDal;
        private readonly List<DateTime> _datesAndTimeSlots = new List<DateTime>();

        public Agenda(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
            _datesAndTimeSlots = GenerateDatesAndTimeSlots();
        }


        public void TryCreateAppointment(int customerId,DateTime appointmentDate, Enums.Type type, Customer customer, Car car)
        {
            if (!IsDateTimeValid(appointmentDate))
            {
                throw new Exception("Invalid date or time");
            }
            Appointment appointment = new Appointment(type,Enums.Status.Scheduled,customer,car);
            _appointmentDal.InsertAppointment(appointment);
        }

        private bool IsDateTimeValid(DateTime dateAndTime)
        {
            List<DateTime> availableDateTimes = GetAvailableDateAndTimeSlots(_datesAndTimeSlots);
            return availableDateTimes.Contains(dateAndTime);
        }

        public List<DateTime> GetAvailableDateAndTimeSlots(List<DateTime> datesAndTimeSlots)
        {
            List<DateTime> availableDatesAndTimeSlots = new List<DateTime>();
            foreach (DateTime dateAndTime in datesAndTimeSlots)
            {
                if (!_appointmentDal.AppointmentExistsByDateAndTime(dateAndTime))
                {
                    availableDatesAndTimeSlots.Add(dateAndTime);
                }
            }
            return availableDatesAndTimeSlots;
        }
        public List<Timeslot> GetAgenda()
        {
            List<DateTime> dateTimes = GenerateDatesAndTimeSlots();
            HashSet<DateTime> availableDateTimes = new HashSet<DateTime>(GetAvailableDateAndTimeSlots(dateTimes));

            return dateTimes.Select(dateAndTime => new Timeslot(dateAndTime, availableDateTimes.Contains(dateAndTime))).ToList();
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