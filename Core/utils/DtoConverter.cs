using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.dto;

namespace Domain.utils
{
    public static class DtoConverter
    {
        public static Customer ConvertCustomerDtoToCustomer(CustomerDto customerDto)
        {
            Customer customer = new Customer(customerDto.FirstName, customerDto.LastName, customerDto.Address, customerDto.Email, customerDto.Password);
            return customer;
        }

        public static Car ConvertCarDtoToCar(CarDto carDto)
        {
            Car car = new Car(carDto.Id,carDto.LicensePlate, carDto.Color, carDto.Brand, carDto.Year);
            return car;
        }

        public static Appointment ConvertAppointmentDtoToAppointment(AppointmentDto appointmentDto)
        {
            Appointment appointment = new Appointment(appointmentDto.Date,appointmentDto.Time,appointmentDto.ServiceType,appointmentDto.Status,ConvertCustomerDtoToCustomer(appointmentDto.Customer),ConvertCarDtoToCar(appointmentDto.Car));
            return appointment;
        }
    }
}