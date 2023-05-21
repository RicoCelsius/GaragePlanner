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

            DateTime dateTime = new DateTime(2019, 1, 1, 8, 0, 0);



            //act
           //agenda.TryAddAppointment(new AppointmentDto(dateTime, Enums.Type.BigMaintenance, Enums.Status.Scheduled,new Customer(), new CarDto()));
            //assert
           // Assert.False(hasAdded);
        }
    }
}
