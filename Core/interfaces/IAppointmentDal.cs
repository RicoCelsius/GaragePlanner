using DAL.dto;

namespace Domain.interfaces;

public interface IAppointmentDal
{
  
    AppointmentDto? GetAppointmentByDate(DateTime date);
    void InsertAppointment(Appointment appointment);
}