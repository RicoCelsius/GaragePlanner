using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.dto
{
    public class AgendaDto
    {
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }


        public AgendaDto(DateTime date, bool isAvailable)
        {
            Date = date;
            IsAvailable = isAvailable;
        }
    }
}
