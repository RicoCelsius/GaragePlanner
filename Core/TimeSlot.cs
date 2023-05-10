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

        public bool HasAppointment()
        {
            return _appointment == null;
        }



        public bool TryAddAppointment(Appointment appointment)
        {
            if (!HasAppointment())
            {
                return false;
            }
            _appointment = appointment;
            return true;
        }



    }
}