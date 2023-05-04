namespace Domain
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public Enums.Type ServiceType { get; set; }
        public Enums.Status Status { get; set; }
        public int? CustomerId { get; set; }
        public int? CarId { get; set; }


        public Appointment(DateTime date, Enums.Type serviceType, Enums.Status status,
            int? customerId, int? carId)
        {
            Date = date;
            ServiceType = serviceType;
            Status = status;
            CustomerId = customerId;
            CarId = carId;
        }
        
        

        public DateTime UpdateAppointmentDate(DateTime newAppointmentDate)
        {
            Date = newAppointmentDate;
            return Date;
        }

        public Enums.Status UpdateAppointmentStatus(Enums.Status newAppointmentStatus)
        {
            Status = newAppointmentStatus;
            return Status;
        }   
    }
}