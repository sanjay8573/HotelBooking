using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Review
{
    public class ReviewReplyRequest
    {
        public int ReviewMasterId { get; set; }
        public int ReviewId { get; set; }
        public int repliedbyId { get; set; }
        public string ReplyText { get; set; }
        public DateTime DateOfReply { get; set; } = DateTime.Now;


    }
}