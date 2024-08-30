using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.OnlineReview
{
    public class ReviewModel
    {
        public int BookingID { get; set; }
        public string BookingDetails { get; set; }
        public int propertyid { get; set; }
        public string PropertyName { get; set; }
        public string  PropertyAdddess { get; set; }
        public string PropertyContactNumber { get; set; }
        public byte[] PropertyImage { get; set; }
        public string CheckinDate { get; set; }
        public string CheckOutDate { get; set; }
        public int TotalAdult { get; set; }
        public int TotalChield { get; set; }
        public int NoOfRooms { get; set; }
        public string RoomTypes { get; set; }
        public string TripType { get; set; }
        public int Clientid { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set;}
        public string ReviewerPhone { get; set; }
        public string ReviewHeading { get; set; }
       
        public string ReviewTextDetail { get; set; }
        public DateTime DateOfReview { get; set; }
        public Byte CleannessRating { get; set; }
        public Byte LocationRating { get; set; }
        public Byte FoodRating { get; set; }
        public Byte StaffRating { get; set; }
        public Byte ServiceRating { get; set; }
        public Byte RoomRating { get; set; }
        public Byte AmenitiesRating { get; set; }
        public bool AlreadySubmitted { get; set; }

    }
    public class RequestJSON{
        public int BookingId { get; set; }
        public int BranchId { get; set; }
        public int GuestId { get; set; }
        public string BookingType { get; set; }


    }
}