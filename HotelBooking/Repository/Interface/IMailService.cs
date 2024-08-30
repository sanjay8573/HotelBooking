using HotelBooking.Model.onlineAPI;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public  interface IMailService
    {
        MailResponse SendMail(MailRequest mailReq);
    
    }
}
