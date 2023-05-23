using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using Domain.interfaces;

namespace GaragePlannerTests.mocks
{
    internal class AppointmentDalMock : IAppointmentDal
    {
        private readonly List<AppointmentDto> _appointmentsInAgenda;

        public AppointmentDalMock(List<AppointmentDto> appointmentsInAgenda)
        {
            this._appointmentsInAgenda = appointmentsInAgenda;


        }
        public bool AppointmentExistsByDateAndTime(DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        

        public List<AppointmentDto> GetAgenda()
        {
            

            
            //appointments.Add(new AppointmentDto(new DateTime(2023, 5, 22, 10, 0, 0), Enums.Type.BigMaintenance, Enums.Status.Scheduled, customer, car));
           // AppointmentDto dto = new AppointmentDto()
           return _appointmentsInAgenda;
        }

        public void InsertAppointment(Appointment appointmentDto)
        {
            
        }
    }
}
