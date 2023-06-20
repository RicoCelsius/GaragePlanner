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
        public DateOnly DateOfDay;
        public readonly List<TimeSlot> TimeSlots;

        public Day(DateOnly dateOfDay)
        {
            DateOfDay = dateOfDay;
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
            TimeOnly endTime = new TimeOnly(17, 0);
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

            if (currentDate == DateOfDay && currentTime > startTime)
            {
                int startIndex = (int)((currentTime - startTime).TotalHours);

                for (int i = 0; i <= startIndex; i++)
                {
                    startTime = startTime.Add(TimeSpan.FromHours(1));
                }
            }

            while (startTime <= endTime)
            {
                if (startTime >= new TimeOnly(9, 0) && startTime <= new TimeOnly(16, 0))
                {
                    TimeSlots.Add(new TimeSlot(startTime));
                }
                startTime = startTime.Add(TimeSpan.FromHours(1));
            }
        }




    }
}