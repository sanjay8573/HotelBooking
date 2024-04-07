using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    public class DashBoardData
    {
        public int NoOfBooking { get; set; }
        public int NoOfGuest { get; set; }
        public int NoOfRooms { get; set; }
        public decimal TodaysRevenue { get; set; }
        public int AvailableRooms { get; set; }
        public int BookedRooms { get; set; }

        public string AvailabilityDate { get; set; }
    }
}