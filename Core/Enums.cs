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
            BrakeRepair,
            TireRotation,
            EngineTuneUp,
            Other
        }

        public enum Status
        {
            Scheduled,
            InProgress,
            Completed,
            Cancelled
        }
    }
}
