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
        public string TripType { get; set; }
        public int Clientid { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set;}
        public string ReviewerPhone { get; set; }
        public string ReviewHeading { get; set; }
       
        public string ReviewTextDetail { get; set; }
        public DateTime DateOfReview { get; set; }
        public Int16 CleannessRating { get; set; }
        public Int16 LocationRating { get; set; }
        public Int16 FoodRating { get; set; }
        public Int16 StaffRating { get; set; }
        public Int16 ServiceRating { get; set; }
        public Int16 RoomRating { get; set; }
        public Int16 AmenitiesRating { get; set; }

    }
    public class RequestJSON{
        public int BookingId { get; set; }
        public int BranchId { get; set; }
        public int GuestId { get; set; }
        public string BookingType { get; set; }


    }
}