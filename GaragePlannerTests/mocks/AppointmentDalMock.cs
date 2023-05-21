using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.interfaces;

namespace GaragePlannerTests.mocks
{
    internal class AppointmentDalMock : IAppointmentDal
    {
        public bool AppointmentExistsByDateAndTime(DateTime dateAndTime)
        {
            throw new NotImplementedException();
        }

        public List<AppointmentDto> GetAgenda()
        {
           // AppointmentDto dto = new AppointmentDto()
           return null;
        }

        public void InsertAppointment(AppointmentDto appointmentDto)
        {
            
        }
    }
}
