using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Infrastructure.Exceptions
{
    public class IdentityServiceException:DomainException
    {
        public IdentityServiceException(string message):base(message)
        {

        }
    }
}
