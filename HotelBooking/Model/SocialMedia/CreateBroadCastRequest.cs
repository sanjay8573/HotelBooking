using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.SocialMedia
{
    public class CreateBroadCastRequest
    {
        public int SocialmediaId { get; set; }
        public byte[] MediaImage { get; set; }
        public string MediaText { get; set; }
        public int BranchId { get; set; }

    }
}