using HotelBooking.Model.OnlineReview;
using HotelBooking.Model.Review;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public  interface IReview
    {
        ReviewModel ReviewRequest(RequestJSON token);
        ReviewResponse SaveReview(ReviewModel reviewEntity);
        IEnumerable<ReviewMaster> GetAllReviews(int branchId);
        IEnumerable<ReviewText> GetAllReviewText(int BranchId);
        ReviewText GetReviewText(int reviewMasterId);
        bool ReviewReply(ReviewReplyRequest req);
        bool ApproveReview(ReviewApproveRequest req);
        IEnumerable<SendForReview> SendForReview(int BranchId);
    }
}
