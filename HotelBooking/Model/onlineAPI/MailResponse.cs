using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.onlineAPI
{
    public class MailResponse
    {
        public string ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public string SenderName { get; set; }
    }
}