using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public static string GetDisplayName(Enum value)
        {
            return Regex.Replace(value.ToString(), "(?<!^)([A-Z])", " $1").ToLowerInvariant();
        }
    }
}
