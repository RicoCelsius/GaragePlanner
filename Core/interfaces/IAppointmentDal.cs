
namespace Domain.interfaces;

public interface IAppointmentDal
{

    void InsertAppointment(AppointmentDto appointmentDto);

    List<AppointmentDto> GetAgenda();
}