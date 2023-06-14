using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.dto;

namespace Domain.utils
{
    public static class DomainConverter
    {
        public static CustomerDto ConvertCustomerToCustomerDto(Customer customer)
        {
            return new CustomerDto(null, customer.FirstName, customer.LastName, customer.Email, customer.Address, customer.Password);
        }

        public static CarDto ConvertCarToCarDto(Car car)
        {
            return new CarDto(car.Id, car.LicensePlate, car.Color, car.Model, car.Year);
        }

        public static AppointmentDto ConvertAppointmentToAppointmentDto(Appointment appointment)
        {
            return new AppointmentDto(appointment.Date,appointment.Time,appointment.ServiceType, appointment.Status, ConvertCustomerToCustomerDto(appointment.Customer), ConvertCarToCarDto(appointment.Car));
        }
    }
}