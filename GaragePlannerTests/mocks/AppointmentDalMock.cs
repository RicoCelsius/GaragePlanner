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
        public bool HasInsertedAppointment = false;

        public AppointmentDalMock(List<AppointmentDto> appointmentsInAgenda)
        {
            this._appointmentsInAgenda = appointmentsInAgenda;


        }
        public bool AppointmentExistsByDateAndTime(DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        

        public Task<List<AppointmentDto>> GetAgendaAsync()
        {
            

     
           return Task.FromResult(_appointmentsInAgenda);
        }

        public void InsertAppointment(AppointmentDto appointmentDto)
        {
            HasInsertedAppointment = true;
        }
    }
}
