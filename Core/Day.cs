using Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Day
    {
        public readonly DateOnly Date;
        public readonly List<TimeSlot> TimeSlots;
        private readonly IAppointmentDal _appointmentDal;

        public Day(DateOnly date)
        {

            Date = date;
            TimeSlots = new List<TimeSlot>();
            InitializeTimeSlots();
        }



        public TimeSlot FindTimeSlot(TimeOnly time)
        {
            return TimeSlots.Find(timeSlot => timeSlot.StartTime == time);
        }

        private void InitializeTimeSlots()
        {
            TimeOnly startTime = new TimeOnly(9, 0);

            for (int i = 0; i < 8; i++)
            {
                TimeSlots.Add(new TimeSlot(startTime));
                startTime = startTime.Add(TimeSpan.FromHours(1));
            }
        }
    }
}