using DAL.dto;

namespace Domain.interfaces;

public interface IAppointmentDal
{
    void Create(AppointmentDto appointment);
    void Delete(int id);
    List<AppointmentDto> GetAll();
}