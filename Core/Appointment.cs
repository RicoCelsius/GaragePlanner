namespace Domain
{
    public class Appointment
    {
        public DateTime DateAndTime { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }

     
        public Enums.Type ServiceType { get; set; }
        public Enums.Status Status { get; set; }



        public Appointment(DateTime dateAndTime,Enums.Type serviceType, Enums.Status status, Customer customer, Car car)
        {
            DateAndTime = dateAndTime;
            ServiceType = serviceType;
            Status = status;
            Customer = customer;
            Car = car;
        }

    }
}