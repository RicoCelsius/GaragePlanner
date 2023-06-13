namespace Domain
{
    public class Appointment
    {
        public DateTime DateAndTime { get; }
        public Enums.Type ServiceType { get;}
        public Enums.Status Status { get; }
        public Customer Customer { get; }
        public Car Car { get; }


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