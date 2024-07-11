using HotelBooking.Model.OnlineReview;
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
    }
}
