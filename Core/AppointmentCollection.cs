using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.dto;

namespace Domain
{
    public class AppointmentCollection
    {
        private readonly IAppointmentDal _appointmentDal;

        public AppointmentCollection(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public void AddAppointment(AppointmentDto appointment)
        {
            _appointmentDal.Create(appointment);
        }

        public void RemoveAppointment(AppointmentDto appointment)
        {
            _appointmentDal.Delete(appointment.Id);
        }

        /*public List<Appointment> GetAppointments()
        {
            throw Exception("Not implemented");
        }*/
    }
}