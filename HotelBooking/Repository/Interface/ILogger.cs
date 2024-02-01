using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    internal interface ILogger
    {
        void Log(string LastError,string errmessage,DateTime dt);
    }
}
