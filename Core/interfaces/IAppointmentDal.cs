using DAL.dto;

namespace Domain.interfaces;

public interface IAppointmentDal
{

    AppointmentDto? GetAppointmentByDateAndTime(DateTime dateAndTime);
    void InsertAppointment(Appointment appointment);
}