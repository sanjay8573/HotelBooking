using HotelBooking.Context;
using HotelBooking.Model.onlineAPI;
using HotelBooking.Model.OnlineReview;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using iTextSharp.tool.xml.html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Web.Http;

namespace HotelBooking.Controllers.onlineAPI
{
    public class ezyHotelController : ApiController
    {
        private readonly IReview _review;
        private readonly CompanyContext _context;
        private IMailService _mailService;

        public ezyHotelController()
        {
            _review = new ReviewRepository();
            _mailService= new MailService();
        }
        [Route("api/online/Review")]
        [HttpPost]
        public ReviewModel ReviewRequest(RequestJSON token)
        {
            ReviewModel rr =_review.ReviewRequest(token);
            return rr;
        }

        [Route("api/online/ReviewSubmit")]
        [HttpPost]
        public ReviewResponse ReviewSubmit(ReviewModel reviewModelEntity)
        {
            ReviewResponse rr = _review.SaveReview(reviewModelEntity);
            return rr;
        }

        [Route("api/online/SendMail")]
        [HttpPost]
        public MailResponse SendMail(MailRequest mailRequestEntity)
        {
            
            return _mailService.SendMail(mailRequestEntity);
        }
    }
}