
namespace Domain.interfaces;

public interface IAppointmentDal
{

    Appointment GetAppointmentByDateAndTime(DateTime dateAndTime);
    bool AppointmentExistsByDateAndTime(DateTime dateAndTime);
    void InsertAppointment(Appointment appointment);
}