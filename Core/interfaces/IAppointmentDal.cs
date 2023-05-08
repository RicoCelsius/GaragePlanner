
namespace Domain.interfaces;

public interface IAppointmentDal
{

    bool AppointmentExistsByDateAndTime(DateTime dateAndTime);
    void InsertAppointment(Appointment appointment);
}