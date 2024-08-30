using HotelBooking.Model.Review;
using HotelBooking.Model.Tour;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.Reviews
{
    public class ReviewController : ApiController
    {
        private readonly IReview _rvw;
        public ReviewController() {
            this._rvw = new ReviewRepository();
        }
        [Route("api/Review/GetAllReviews")]
        [HttpGet]
        public IEnumerable<ReviewMaster> GetAllReviews(int branchId)
        {
            return _rvw.GetAllReviews(branchId);
        }
        [Route("api/Review/GeReviewText")]
        [HttpGet]
        public ReviewText GeReviewText(int reviewId)
        {
            return _rvw.GetReviewText(reviewId);
        }

        [Route("api/Review/ReplyToReview")]
        [HttpPost]
        public bool ReplyToReview(ReviewReplyRequest req)
        {
            return _rvw.ReviewReply(req);
        }
        [Route("api/Review/ApproveReview")]
        [HttpPost]
        public bool ApproveReview(ReviewApproveRequest req)
        {
            return _rvw.ApproveReview(req);
        }

        [Route("api/Review/CustomerToReview")]
        [HttpGet]
        public IEnumerable<SendForReview> CustomerToReview(int branchId)
        {
            return _rvw.SendForReview(branchId);
        }
        
    }
}
