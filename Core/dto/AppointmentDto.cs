using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
