
namespace Domain.interfaces;

public interface IAppointmentDal
{

    bool AppointmentExistsByDateAndTime(DateTime dateAndTime);
    void InsertAppointment(int? id,AppointmentDto appointmentDto);

    List<AppointmentDto> GetAgenda();
}