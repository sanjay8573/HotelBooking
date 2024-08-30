using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.onlineAPI
{
    public class MailRequest
    {
        public string MailText { get; set; }
        public string MailSubject { get; set; }
        public string  SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set;}
    }
}