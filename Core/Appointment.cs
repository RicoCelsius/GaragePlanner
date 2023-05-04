namespace Domain
{
    public class Appointment
    {
        private DateTime AppointmentDate { get; set; }
        private Enums.Type ServiceType { get; set; }
        private Enums.Status AppointmentStatus { get; set; }
        private int? CustomerId { get; set; }
        private int? CarId { get; set; }


        public Appointment(DateTime appointmentDate, Enums.Type serviceType, Enums.Status appointmentStatus,
            int? customerId, int? carId)
        {
            AppointmentDate = appointmentDate;
            ServiceType = serviceType;
            AppointmentStatus = appointmentStatus;
            CustomerId = customerId;
            CarId = carId;
        }
        
        

        public DateTime UpdateAppointmentDate(DateTime newAppointmentDate)
        {
            AppointmentDate = newAppointmentDate;
            return AppointmentDate;
        }

        public Enums.Status UpdateAppointmentStatus(Enums.Status newAppointmentStatus)
        {
            AppointmentStatus = newAppointmentStatus;
            return AppointmentStatus;
        }   
    }
}