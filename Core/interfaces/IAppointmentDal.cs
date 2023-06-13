
namespace Domain.interfaces;

public interface IAppointmentDal
{

    void InsertAppointment(AppointmentDto appointment);

    List<AppointmentDto> GetAgenda();
    List<AppointmentDto> GetAgendaOfDay(DateOnly date);
}