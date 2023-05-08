using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Day
    {
        public readonly DateTime Date;
        public readonly List<TimeSlot> TimeSlots;

        public Day(DateTime date)
        {
            Date = date;
            TimeSlots = new List<TimeSlot>();
            InitializeTimeSlots();
        }

        public bool IsDayAvailable()
        {
            foreach (var slot in TimeSlots)
            {
                if (slot.IsAvailable())
                {
                    return true;
                }
            }
            return false;
        }


        private void InitializeTimeSlots()
        {
            DateTime startTime = Date.Date.AddHours(9);

            for (int i = 0; i < 8; i++)
            {
                TimeSlots.Add(new TimeSlot(startTime));
                startTime = startTime.AddHours(1);
            }
        }


    }

}

