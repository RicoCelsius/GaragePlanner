namespace Domain
{
    public class TimeSlot
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        private Appointment? _appointment;

        public TimeSlot(DateTime startTime)
        {
            StartTime = startTime;
            EndTime = StartTime.AddHours(1);
        }

        public bool IsAvailable()
        {
            return _appointment == null;
        }


        public bool tryAddAppointment(Appointment appointment)
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