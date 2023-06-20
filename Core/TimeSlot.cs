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

        public void AddAppointment(Appointment appointment)
        {
            if (IsAvailable())
            {
                _appointment = appointment;
            }
        }
    }
}