using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.dto
{
    public class AppointmentDto
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public DateTime? Date { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
       

        public AppointmentDto(int id, int customerId, int vehicleId, DateTime date, int type, int status)
        {
            Id = id;
            CustomerId = customerId;
            VehicleId = vehicleId;
            Date = date;
            Type = type;
            Status = status;
        }
    }
}
