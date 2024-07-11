using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.OnlineReview;
using HotelBooking.Model.Review;
using HotelBooking.Repository.Interface;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class ReviewRepository : IReview
    {
        private readonly CompanyContext _context;
        //public ReviewRepository(CompanyContext context)
        //{
        //    this._context = context;
        //}
        public ReviewRepository()
        {
            this._context = new CompanyContext();
        }
        public ReviewModel ReviewRequest(RequestJSON jsonString)
        {
          
            //string reqJson= Helper.HotelBookingHelper.Base64Decode(token);
            //RequestJSON jsonString = JsonConvert.DeserializeObject<RequestJSON>(reqJson);
            Booking booking = _context.Booking.Where(b => b.BookingId == jsonString.BookingId).SingleOrDefault();
            Guests guest= _context.Guests.Where(g=>g.GuestId==jsonString.GuestId).SingleOrDefault();
            Branch hotel=_context.Branch.Where(b=>b.Id==jsonString.BranchId).SingleOrDefault();
            ReviewModel resModel= new ReviewModel();
            resModel.BookingID = booking.BookingId;
            resModel.BookingDetails = "Your Stay at "+hotel.BranchName + " From "+booking.CheckIn +" To "+booking.Checkout +" in "+booking.BookingTypeName + " For " + booking.Nights;
            resModel.Clientid = guest.GuestId;
            resModel.TripType = booking.BookingTypeName;
            resModel.ReviewerName = guest.Name;
            resModel.ReviewerPhone = guest.Phone;
            resModel.ReviewerEmail = guest.email;
            resModel.propertyid = hotel.Id;
            return resModel;


        }

        public ReviewResponse SaveReview(ReviewModel reviewEntity)
        {
            ReviewResponse rr= new ReviewResponse();
           
           if (reviewEntity != null) {
                rr.Reviews = reviewEntity;
                ReviewMaster RM = new ReviewMaster();
                RM.BookingID = reviewEntity.BookingID;
                RM.propertyid = reviewEntity.propertyid;
                RM.Clientid=reviewEntity.Clientid;
                RM.ReviewerEmail=reviewEntity.ReviewerEmail;
                RM.ReviewerPhone=reviewEntity.ReviewerPhone;
                RM.ReviewerName=reviewEntity.ReviewerName;
                RM.TripType=reviewEntity.TripType;
                RM.isActive = true;
                RM.DateCreated = DateTime.Now;
                _context.ReviewMaster.Add(RM);
                _context.SaveChanges();
                ReviewText RT= new ReviewText();
                RT.MasterID = RM.MasterID;
                RT.ReviewHeading = reviewEntity.ReviewHeading;
                RT.ReviewTextDetail=reviewEntity.ReviewTextDetail;
                RT.DateOfReview = DateTime.Now;
                RT.CleannessRating=reviewEntity.CleannessRating;
                RT.AmenitiesRating=reviewEntity.AmenitiesRating;
                RT.FoodRating=reviewEntity.FoodRating;
                RT.LocationRating=reviewEntity.LocationRating;
                RT.StaffRating=reviewEntity.StaffRating;
                RT.ServiceRating=reviewEntity.ServiceRating;
                RT.RoomRating=reviewEntity.RoomRating;
                RT.isActive = true;
                RT.DateCreated=DateTime.Now;
                _context.ReviewText.Add(RT);
                _context.SaveChanges();
            }

            return rr;
        }
    }
}