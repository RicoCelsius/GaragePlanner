namespace Domain
{
    public class Appointment
    {
        public Enums.Type ServiceType { get; set; }
        public Enums.Status Status { get; set; }

        public Customer Customer { get; set; }
        public Car Car { get; set; }



        public Appointment(Enums.Type serviceType, Enums.Status status,Customer customer, Car car)
       
        {
            ServiceType = serviceType;
            Status = status;
            Customer = customer;
            Car = car;

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