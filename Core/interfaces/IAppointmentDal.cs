﻿
namespace Domain.interfaces;

public interface IAppointmentDal
{

    void InsertAppointment(Appointment appointment);

    List<AppointmentDto> GetAgenda();
}