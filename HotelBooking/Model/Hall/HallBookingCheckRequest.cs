using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Hall
{
    public class HallBookingCheckRequest
    {
        public int HallId { get; set; }
        public DateTime BookingDate  { get; set; }
        public int SlotId { get; set; }
        public int BranchId { get; set; }
    }

    public class HallBookingCheckResponse
    {
        public IEnumerable<HallBooking> HallBooked { get; set; }
        public bool isAvailable { get; set; }
    }
}