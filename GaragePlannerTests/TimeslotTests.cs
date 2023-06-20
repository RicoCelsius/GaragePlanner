using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaragePlannerTests
{
    public class TimeslotTests
    {
        [Fact]
        public void AddAppointmentButTimeSlotIsNotAvailable()
        {
            // Arrange
            var date = new DateOnly(2023, 1, 1); 
            var startTime = new TimeOnly(10, 0, 0); 
            Customer customer = new Customer("Rico", "Aarntzen", "Straatnaam 10", "", "");
            Car car = new Car("AB-12-CD", Enums.Color.Blue, "Audi", 2019);
            var appointment = new Appointment(date,startTime,Enums.Type.BigMaintenance,Enums.Status.InProgress,customer,car); 

            var timeSlot = new TimeSlot(startTime);

            // Act
            timeSlot.AddAppointment(appointment);

            // Assert
            Assert.False(timeSlot.IsAvailable());
        }

    }
}
