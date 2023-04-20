using DAL.dto;

namespace DAL;

public interface IAppointmentDal
{
    void Create(AppointmentDto appointment);
    void Delete(int id);
    List<AppointmentDto> GetAll();
}