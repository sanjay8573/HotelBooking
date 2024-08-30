using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Review
{
    public class ReviewApproveRequest
    {
        public int MasterID { get; set; }
        public int ApprovedBy { get; set;}
        public DateTime ApprovedDate { get; set; } = DateTime.Now;
    }
}