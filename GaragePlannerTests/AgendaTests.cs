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
            AppointmentCollection appointmentCollection = new AppointmentCollection(appointmentDalMock);
            DateOnly date = appointmentCollection.Days[0].DateOfDay;
            TimeOnly time = appointmentCollection.Days[0].TimeSlots[0].StartTime;
            Customer customer = new Customer("Rico","Aarntzen","Straatnaam 10","ricoaarntzen@gmail.com","lol123456");
            Car car = new Car("AB-12-CD",Enums.Color.Blue,"Audi",2019);

            //act
            bool hasAdded = appointmentCollection.TryCreateAppointment(date, time, Enums.Type.BigMaintenance, customer, car);
            
            //assert
            Assert.True(hasAdded);
            Assert.True(appointmentDalMock.HasInsertedAppointment);
        }

        [Fact]
        public void AddAppointmentInNonEmptyTimeSlot()
        {
            // Arrange
            DateOnly date = DayGenerator.GenerateDays(1)[0].DateOfDay;
            TimeOnly time = new TimeOnly(16, 0);
            CustomerDto customerDto = new CustomerDto(1,"Rico", "Aarntzen", "Straatnaam 10", "", "");
            CarDto carDto = new CarDto(1,"AB-12-CD", Enums.Color.Blue, "Audi", 2019);
            List<AppointmentDto> appointmentsOfDay = new List<AppointmentDto>();
            appointmentsOfDay.Add(new AppointmentDto(1,date, time, Enums.Type.BigMaintenance, Enums.Status.Scheduled,customerDto,carDto));

            AppointmentDalMock appointmentDalMock = new AppointmentDalMock(appointmentsOfDay);
            AppointmentCollection appointmentCollection = new AppointmentCollection(appointmentDalMock);

            Customer customer = new Customer("Rico", "Aarntzen", "Straatnaam 10", "", "");
            Car car = new Car("AB-12-CD", Enums.Color.Blue, "Audi", 2019);


            // Act
            bool hasAdded = appointmentCollection.TryCreateAppointment(date, time, Enums.Type.BigMaintenance, customer, car);

            // Assert
            Assert.False(hasAdded);
            Assert.False(appointmentDalMock.HasInsertedAppointment);
        }


        [Fact]
        public void AreAmountOfDaysGeneratedProperly()
        {
            //arrange
            AppointmentDalMock appointmentDalMock = new AppointmentDalMock(new List<AppointmentDto>());
            AppointmentCollection appointmentCollection = new AppointmentCollection(appointmentDalMock);
            int amountOfDays = appointmentCollection.Days.Count;
            //act
            List<Day> days = DayGenerator.GenerateDays(amountOfDays);
            //assert
            Assert.Equal(amountOfDays, days.Count);
        }

        [Fact]
        public void AreThereWeekDays()
        {
            //arrange
            AppointmentDalMock appointmentDalMock = new AppointmentDalMock(new List<AppointmentDto>());
            AppointmentCollection appointmentCollection = new AppointmentCollection(appointmentDalMock);
            int amountOfDays = appointmentCollection.Days.Count;
            //act
            List<Day> days = DayGenerator.GenerateDays(amountOfDays);
            //assert
            Assert.DoesNotContain(days, day => day.DateOfDay.DayOfWeek == DayOfWeek.Saturday || day.DateOfDay.DayOfWeek == DayOfWeek.Sunday);
        }


      /*  [Fact]
        public void DoesAgendaLoadCorrectly()
        {
            // Arrange
            var someDate = DateOnly.FromDateTime(DateTime.Now);
            var someTime = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12));

            var appointmentDto = new AppointmentDto
            {
                Date = someDate,
                Time = someTime,
                serviceType = Enums.Type.BigMaintenance,
              

                
            };

            var appointmentDal = new AppointmentDalMock(new List<AppointmentDto>());

            var days = new List<Day> { new Day { DateOfDay = someDate } };
            var myClass = new AppointmentCollection(appointmentDal); 

            // Act
            myClass.LoadAgenda();

            // Assert
            var targetDay = days.FirstOrDefault(d => d.DateOfDay.Equals(appointmentDto.Date));
            Assert.NotNull(targetDay);
            var targetTimeSlot = targetDay.FindTimeSlot(appointmentDto.Time);
            Assert.NotNull(targetTimeSlot);
            Assert.Contains(targetTimeSlot.Appointments, a => a.Date == appointmentDto.Date && a.Time == appointmentDto.Time);
        }*/


    }
}
