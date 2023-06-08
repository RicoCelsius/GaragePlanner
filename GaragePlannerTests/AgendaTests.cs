using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using Domain.utils;
using GaragePlannerTests.mocks;

namespace GaragePlannerTests
{
    public class AgendaTests
    {

        [Fact]
        public void AddAppointmentInEmptyTimeSlot()
        {
            //arrange
            AppointmentDalMock appointmentDalMock = new AppointmentDalMock(new List<AppointmentDto>());
            DateTime now = DateTime.Now;
            DateTime targetDateTime = new DateTime(now.Year, now.Month, now.Day, 10, 0, 0);

            Agenda agenda = new Agenda(appointmentDalMock);
            Customer customer = new Customer("Rico","Aarntzen","Straatnaam 10","ricoaarntzen@gmail.com","lol123456");
            Car car = new Car("AB-12-CD",Enums.Color.Black,"Audi",2019);

            //act
            Result hasAdded = agenda.CreateAppointment(new Appointment(targetDateTime, Enums.Type.BigMaintenance, Enums.Status.Scheduled, customer, car));
            
            //assert
            Assert.True(hasAdded.Success);
            Assert.True(appointmentDalMock.HasInsertedAppointment);
        }

        [Fact]
        public void AddAppointmentInNonEmptyTimeSlot()
        {
            //arrange

            DateTime now = DateTime.Now;
            DateTime targetDateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour+1, 0, 0);



            CustomerDto customerDto = new CustomerDto(1, "Rico", "Aarntzen", "Straatnaam", "Straatnaam 10", "lol123456");
            CarDto carDto = new CarDto(1, "AB-12-CD", Enums.Color.Black, "Audi", 2019);
            AppointmentDto appointmentDto = new AppointmentDto(targetDateTime,Enums.Type.BigMaintenance, Enums.Status.Scheduled, customerDto, carDto);
            List<AppointmentDto> appointmentDtos = new List<AppointmentDto>();
            appointmentDtos.Add(appointmentDto);
            AppointmentDalMock appointmentDalMock = new AppointmentDalMock(appointmentDtos);

            Agenda agenda = new Agenda(appointmentDalMock);

            Customer customer = new Customer("Rico", "Aarntzen", "Straatnaam 10", "ricoaarntzen@gmail.com", "lol123456");
            Car car = new Car("AB-12-CD", Enums.Color.Black, "Audi", 2019);


            //act
            Result hasAdded = agenda.CreateAppointment(new Appointment(targetDateTime, Enums.Type.BigMaintenance, Enums.Status.Scheduled, customer, car));

            Assert.False(hasAdded.Success);
        }

        [Fact]
        public void AreDaysGeneratedProperly()
        {
            //arrange
            AppointmentDalMock appointmentDalMock = new AppointmentDalMock(new List<AppointmentDto>());
            Agenda agenda = new Agenda(appointmentDalMock);
            int amountOfDays = agenda.Days.Count;
            //act
            List<Day> days = DayGenerator.GenerateDays(amountOfDays);
            //assert
            Assert.Equal(amountOfDays, days.Count);
        }

    }
}
