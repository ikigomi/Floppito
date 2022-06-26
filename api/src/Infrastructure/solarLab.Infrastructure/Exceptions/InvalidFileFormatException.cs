using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Infrastructure.Exceptions
{
    public class InvalidFileFormatException : DomainException
    {
        public InvalidFileFormatException(string message) : base(message)
        {

        }
    }
}
