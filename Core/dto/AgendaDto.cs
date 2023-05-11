using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.dto
{
    public class AgendaDto
    {
        DateTime Date { get; set; }
        bool IsAvailable { get; set; }


        public AgendaDto(DateTime date, bool isAvailable)
        {
            Date = date;
            IsAvailable = isAvailable;
        }
    }
}
