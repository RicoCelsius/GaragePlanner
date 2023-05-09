using System.Collections.Generic;
using System.Linq;
using Domain.interfaces;

namespace Domain
{
    public class AppointmentCollection
    {
        private readonly IAppointmentDal _appointmentDal;

        public AppointmentCollection(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }





        /*public List<Appointment> GetAppointments()
        {
            throw Exception("Not implemented");
        }*/
    }
}