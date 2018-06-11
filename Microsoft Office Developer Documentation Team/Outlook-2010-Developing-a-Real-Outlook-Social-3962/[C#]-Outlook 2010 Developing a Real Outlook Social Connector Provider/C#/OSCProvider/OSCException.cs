using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSCProvider
{
    public class OSCException:Exception 
    {
        public OSCException(string message, OSCExceptions ex, Exception innerException = null):base(message,innerException)
        {
            HResult = Convert.ToInt32(ex);
        }
    }
}
