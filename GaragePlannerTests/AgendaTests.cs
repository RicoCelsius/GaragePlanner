using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using GaragePlannerTests.mocks;

namespace GaragePlannerTests
{
    public class AgendaTests
    {
        [Fact]
        public void AddAppointmentButTimeslotIsFull()
        {
            //arrange
            Agenda agenda = new Agenda(new AppointmentDalMock());
            CustomerMock customerMock = new CustomerMock();
            CarMock carMock = new CarMock();
            List<CustomerDto> customers = customerMock.GenerateCustomers(2);
            List<CarDto> cars = carMock.GenerateCarDto(2);
            DateTime dateTime = new DateTime(2019, 1, 1, 8, 0, 0);

            //agenda.CreateAppointment(new AppointmentDto(dateTime, Enums.Type.BigMaintenance, Enums.Status.Scheduled, customers[0], cars[0]), customers[0]);


            //act
           // bool hasAdded = agenda.CreateAppointment(new AppointmentDto(dateTime, Enums.Type.BigMaintenance, Enums.Status.Scheduled, customers[0], cars[0]), customers[0]);
            //assert
           // Assert.False(hasAdded);
        }
    }
}
