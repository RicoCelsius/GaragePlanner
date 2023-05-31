using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Enums
    {
        public enum Type
        {
            OilChange,
            BigMaintenance,
            SmallMaintenance,
        }

        public enum Status
        {
            Scheduled,
            InProgress,
            Completed,
            Cancelled
        }

        public enum Color{
            Red,
            Blue,
            Green,
            Yellow,
            Black,
            White,
            Grey,
            Orange,
            Purple,
            Pink,
            Brown
        }
    }
}
