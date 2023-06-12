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
        public bool HasInsertedAppointment;

        public AppointmentDalMock(List<AppointmentDto> appointmentsInAgenda)
        {
            HasInsertedAppointment = false;
            this._appointmentsInAgenda = appointmentsInAgenda;

        }
        public bool AppointmentExistsByDateAndTime(DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        

        public List<AppointmentDto> GetAgenda()
        {
            

     
           return _appointmentsInAgenda;
        }

        public void InsertAppointment(AppointmentDto appointmentDto)
        {
            HasInsertedAppointment = true;
            
        }
    }
}
