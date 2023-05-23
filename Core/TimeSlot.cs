using Domain.interfaces;

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

        public bool CanAddAppointment(Appointment appointment)
        {
            if (!IsAvailable())
            {
                return false;
            }
            _appointment = appointment;
            return true;
        }
    }
}