namespace Domain
{
    public class Appointment
    {
        public DateTime DateAndTime { get; set; }
        public Enums.Type ServiceType { get; set; }
        public Enums.Status Status { get; set; }
        public int? CustomerId { get; set; }
        public int? CarId { get; set; }


        public Appointment(DateTime date, Enums.Type serviceType, Enums.Status status,
            int? customerId, int? carId)
        {
            DateAndTime = date;
            ServiceType = serviceType;
            Status = status;
            CustomerId = customerId;
            CarId = carId;
        }
        
        

        public DateTime UpdateAppointmentDate(DateTime newAppointmentDate)
        {
            DateAndTime = newAppointmentDate;
            return newAppointmentDate;
        }

        public Enums.Status UpdateAppointmentStatus(Enums.Status newAppointmentStatus)
        {
            Status = newAppointmentStatus;
            return Status;
        }   
    }
}