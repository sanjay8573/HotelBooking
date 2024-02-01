using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace HotelBooking.Model
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string validToen { get; set; }
    }
}