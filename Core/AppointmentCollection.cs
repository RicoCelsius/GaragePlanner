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


        public void TryCreateAppointment(int customerId,DateTime appointmentDate, Enums.Type type)
        {
            if (AppointmentValidator.IsDateTimeValid(appointmentDate))
            {
                throw new Exception("Invalid date or time");
            }
            Appointment appointment = new Appointment(appointmentDate,type,Enums.Status.Scheduled,customerId,1);
            _appointmentDal.InsertAppointment(appointment);
        }












        /*public List<Appointment> GetAppointments()
        {
            throw Exception("Not implemented");
        }*/
    }
}