namespace Domain
{
    public class Appointment
    {
        public DateOnly Date { get; }
        public TimeOnly Time { get; }
        public Enums.Type ServiceType { get;}
        public Enums.Status Status { get; }
        public Customer Customer { get; }
        public Car Car { get; }


        public Appointment(DateOnly date, TimeOnly time, Enums.Type serviceType, Enums.Status status, Customer customer, Car car)
        {
            Date = date;
            Time = time;
            ServiceType = serviceType;
            Status = status;
            Customer = customer;
            Car = car;
        }
    }
}