using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.SocialMedia
{
    public class BroadCastResponse
    {
        public string ResponseMessage { get; set; }
        IEnumerable<BroadCastMediaResponse> broadCastMediaResponses { get; set; }
        public BroadCastResponse() {
            broadCastMediaResponses= new List<BroadCastMediaResponse>();
        }

    }
    public class BroadCastMediaResponse
    {
        public int SocialmediaId { get; set; }
        public bool isSuccess { get; set; }
        public string Reason { get; set; }
    }
}