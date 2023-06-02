using Domain.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppointmentDto
    {
        public int? Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public Enums.Type ServiceType { get; set; }
        public Enums.Status Status { get; set; }
        public CustomerDto Customer { get; set; }
        public CarDto Car { get; set; }

        public AppointmentDto(DateTime date, Enums.Type serviceType, Enums.Status status, CustomerDto customer, CarDto car)
        {
            DateAndTime = date;
            ServiceType = serviceType;
            Status = status;
            Customer = customer;
            Car = car;
        }
    }
}

