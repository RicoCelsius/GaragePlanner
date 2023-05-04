using DAL.dto;

namespace Domain.interfaces;

public interface IAppointmentDal
{
    void Create(AppointmentDto appointment);
    AppointmentDto GetAppointmentByDate(DateTime date);
    void Delete(int id);

}