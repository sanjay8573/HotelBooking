using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.OnlineReview
{
    public class ReviewResponse
    {
        public ReviewModel Reviews { get; set; }
        public string status { get; set; }
    }
}