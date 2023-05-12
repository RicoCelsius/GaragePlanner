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
            Customer customer = new Customer(customerDto.Email, customerDto.Password, customerDto.FirstName, customerDto.LastName, customerDto.Password);
            return customer;
        }

        public static Car ConvertCarDtoToCar(CarDto carDto)
        {
            Car car = new Car(carDto.LicensePlate, carDto.Color, carDto.Model, carDto.Year);
            return car;
        }

        public static Appointment ConvertAppointmentDtoToAppointment(AppointmentDto appointmentDto)
        {
            Appointment appointment = new Appointment(appointmentDto.ServiceType, appointmentDto.Status, ConvertCustomerDtoToCustomer(appointmentDto.Customer), ConvertCarDtoToCar(appointmentDto.Car));
            return appointment;
        }
    }
}