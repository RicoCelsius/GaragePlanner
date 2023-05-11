using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Timeslot

    {

        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        public Appointment Appointment { get; set; }



        public Timeslot(DateTime date, bool isAvailable, Appointment appointment)
        {
            Date = date;
            IsAvailable = isAvailable;
            Appointment = appointment;
        }
    }
}
