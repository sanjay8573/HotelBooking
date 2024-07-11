using HotelBooking.Context;
using HotelBooking.Model.onlineAPI;
using HotelBooking.Model.OnlineReview;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.onlineAPI
{
    public class ezyHotelController : ApiController
    {
        private readonly IReview _review;
        private readonly CompanyContext _context;

        public ezyHotelController()
        {
            _review = new ReviewRepository();
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
    }
}