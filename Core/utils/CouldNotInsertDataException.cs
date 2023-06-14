using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.utils
{
    public class CouldNotInsertDataException : Exception
    {
        private Exception ex;
        public CouldNotInsertDataException(string message, Exception ex) : base(message)
        {
            this.ex = ex;
        }
    }
}
