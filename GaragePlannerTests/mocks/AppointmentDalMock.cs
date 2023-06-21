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
        private readonly List<AppointmentDto> _appointmentsOfDay;
        public bool HasInsertedAppointment;



        public AppointmentDalMock(List<AppointmentDto> appointmentsOfDay)
        {
            HasInsertedAppointment = false;
            _appointmentsOfDay = appointmentsOfDay;
        }

        public AppointmentDalMock(List<AppointmentDto> appointmentsInAgenda, List<AppointmentDto> appointmentsOfDay)
        {
            _appointmentsInAgenda = appointmentsInAgenda;
            _appointmentsOfDay = appointmentsOfDay;
        }



        public List<AppointmentDto> GetAgenda()
        {
            return _appointmentsInAgenda;
        }

        public List<AppointmentDto> GetAgendaOfDay(DateOnly date)
        {
            return _appointmentsOfDay;
        }

        public void InsertAppointment(Appointment appointmentDto)
        {
            HasInsertedAppointment = true;
            
        }
    }
}
