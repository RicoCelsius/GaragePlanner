using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.utils
{
    public class CouldNotDeleteDataException : Exception
    {
        private Exception ex;

        public CouldNotDeleteDataException(string message, Exception ex) : base(message)
        {
            this.ex = ex;
        }
    }
}
