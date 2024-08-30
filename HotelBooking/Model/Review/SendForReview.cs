using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Review
{
    public class SendForReview
    {
        public int BookedRoomId { get; set; }
        public int BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int BookingTypeId { get; set; }
        public string BookingType { get; set; }
        public int GuestId { get; set; }
        public string GuestName { get; set;}
        public string GuestEmail { get; set; }
        public string GuestPhone { get; set; }
        public string RoomNumber { get; set; }
        public string RoomTypeName { get; set; }
        public int BranchId { get; set; }


    }
}