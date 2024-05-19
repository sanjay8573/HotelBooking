using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.onlineAPI
{
    public class RoomRequest
    {
        public string HotelID { get; set; }
        public string BranchID { get; set;}
        public string CheckOutDate { get; set; }
        public string CheckInDate { get; set;}
        public Authentication Authentication { get; set; }
        public RoomGroup RoomGroupAry { get; set; }
    }
    public class Authentication
    {
        public string username { get; set; }
        public string password { get; set; }
        public string APIKey { get; set;}
        public string UserHostAddress { get; set; }
        public string UserAgent { get; set;}
        public string UserHostName { get; set; }
    }
    public class RoomGroup
    {
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public string ChildrenAges { get; set;}
    }
}