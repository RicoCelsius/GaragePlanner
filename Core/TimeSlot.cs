using Domain.interfaces;
using Domain.utils;

namespace Domain
{
    public class TimeSlot
    {
        public TimeOnly StartTime { get; }

        private Appointment? _appointment;

        public TimeSlot(TimeOnly startTime)
        {
            StartTime = startTime;
        }

        public bool IsAvailable()
        {
            return _appointment == null;
        }

        public Result AddAppointment(Appointment appointment)
        {
            if (!IsAvailable())
            {
                return new Result(false, "Timeslot already full");
            }
            _appointment = appointment;
            return new Result(true, "Appointment added");
        }
    }
}