
namespace Domain.interfaces;

public interface IAppointmentDal
{

    void InsertAppointment(AppointmentDto appointment);

    Task<List<AppointmentDto>> GetAgendaAsync();
}