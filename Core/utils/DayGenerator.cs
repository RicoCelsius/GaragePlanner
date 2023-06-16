using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.utils
{
    public static class DayGenerator
    {
        public static List<Day> GenerateDays(int amountOfDays)
        {
            List<Day> days = new List<Day>();
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

            while (days.Count < amountOfDays)
            {
                if (!IsWeekend(startDate))
                {
                    days.Add(new Day(startDate));
                }

                startDate = startDate.AddDays(1);
            }

            return days;
        }

        private static bool IsWeekend(DateOnly date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}
