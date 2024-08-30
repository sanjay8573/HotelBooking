using HotelBooking.Model.onlineAPI;
using HotelBooking.Repository.Interface;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace HotelBooking.Repository.Implementation
{
    public class MailService : IMailService
    {
        public MailResponse SendMail(MailRequest mailReq)
        {
            MailResponse mailResp = new MailResponse();
            try
            {
                // Set up SMTP client
                SmtpClient client = new SmtpClient();
                client.Host = "relay-hosting.secureserver.net";
                client.Port = 25;
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("sanjay_gope@hotmail.com", "Plmnbvcxzaq@12345");

                // Create email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("info@logicfinders.com");
                mailMessage.To.Add(mailReq.SenderEmail);
                mailMessage.Subject = mailReq.MailSubject;
                mailMessage.IsBodyHtml = true;
                
                
                mailMessage.Body = mailReq.MailText.ToString();

                // Send email
                client.Send(mailMessage);
                mailResp.ResponseMessage = "Mail Sent to Customer for Review !!!";
                mailResp.ResponseStatus = "Success";
                // Clean up.
                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                mailResp.ResponseStatus = "Failed";
                mailResp.ResponseMessage = "Error" + ex.Message.ToString();
                // Exception Details
                return mailResp;
            }
            return mailResp;
        }
        
    }
}