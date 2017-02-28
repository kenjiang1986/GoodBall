using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class ServiceException : ApplicationException
    {
        public ServiceException(string errormessage)
            : base(errormessage)
        {

        }
    }
}
