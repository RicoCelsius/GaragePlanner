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
            List<Day> Days = new List<Day>();
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
            for (int i = 0; i < amountOfDays; i++)
            {
                Days.Add(new Day(startDate));
                startDate = startDate.AddDays(1);
            }
            return Days;
        }
    }
}
