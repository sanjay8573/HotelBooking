using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.SocialMedia
{
    public class BroadCastRequest
    {
        public int BranchId { get; set; }
        public string BradCastLink { get; set; }
        public string BradCastText { get; set; }
        public List<string> SocialMediaIds { get; set; }

    }
}