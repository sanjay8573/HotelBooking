using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace HotelBooking.Model
{
    public class HouseKeeping
    {
        public int HouseKeepingId { get; set; }
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public int FloorId { get; set; }
        public string RoomNumber { get; set; }  
        public string RoomTypeName { get; set;}
        public string FloorName { get; set;}
        public string HouseKeepingStatus { get; set;}

        public int  AssignedTo { get; set; }
        public string AssignedToName { get; set; }
        public string status { get; set; }
        public int BramchId { get; set; }




    }
}