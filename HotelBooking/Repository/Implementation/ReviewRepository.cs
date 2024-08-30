using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.OnlineReview;
using HotelBooking.Model.Reatraurant;
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

            bool AlreadySubmitted = false;
            int  _reviewCount = _context.ReviewMaster.Where(r => r.BookingID == jsonString.BookingId && r.propertyid == jsonString.BranchId).Count();
            if (_reviewCount > 0)
            {
                AlreadySubmitted = true;
            }
            
            //string reqJson= Helper.HotelBookingHelper.Base64Decode(token);
            //RequestJSON jsonString = JsonConvert.DeserializeObject<RequestJSON>(reqJson);
            Booking booking = _context.Booking.Where(b => b.BookingId == jsonString.BookingId).SingleOrDefault();
            Guests guest= _context.Guests.Where(g=>g.GuestId==jsonString.GuestId).SingleOrDefault();
            Branch hotel=_context.Branch.Where(b=>b.Id==jsonString.BranchId).SingleOrDefault();
            ImageMaster _hotelImage=_context.ImageMaster.Where(i=>i.BranchId==jsonString.BranchId && i.ImageTypeId==0  && i.RefId==jsonString.BranchId && i.isMainImage == true).SingleOrDefault();
            ReviewModel resModel= new ReviewModel();

            resModel.BookingID = booking.BookingId;
            resModel.BookingDetails = "Your Stay at "+hotel.BranchName + " From "+booking.CheckIn +" To "+booking.Checkout +" in "+booking.BookingTypeName + " For " + booking.Nights;
            resModel.Clientid = guest.GuestId;
            resModel.PropertyName = hotel.BranchName;
            resModel.PropertyAdddess = hotel.Address + " " + hotel.City + " " + hotel.Country + "-" + hotel.Postcode;
            resModel.PropertyContactNumber = hotel.Postcode;
            resModel.PropertyImage = _hotelImage.ImageData;
            resModel.TripType = "";
            resModel.ReviewerName = guest.Name;
            resModel.ReviewerPhone = guest.Phone;
            resModel.ReviewerEmail = guest.email;
            resModel.propertyid = hotel.Id;
            resModel.AlreadySubmitted = AlreadySubmitted;
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

        public IEnumerable<ReviewMaster> GetAllReviews(int branchId)
        {
            return _context.ReviewMaster.Where(b => b.propertyid == branchId).ToArray();
        }
        public IEnumerable<ReviewText> GetAllReviewText(int BranchId)
        {
            return _context.ReviewText.Where(b=>b.MasterID==BranchId).ToArray();
        }
        public ReviewText GetReviewText(int reviewMasterId)
        {
           return _context.ReviewText.Where(r => r.MasterID == reviewMasterId).SingleOrDefault();

        }
        public bool ReviewReply(ReviewReplyRequest req)
        {
            bool rtnVal = false;
            ReviewText tmpReview = _context.ReviewText.Find(req.ReviewId);
            if (tmpReview != null)
            {
                tmpReview.ReplyText = req.ReplyText;
                tmpReview.repliedbyId = req.repliedbyId;
                tmpReview.DateOfReply = req.DateOfReply;
                tmpReview.IsReply = true;
                _context.SaveChanges();
                rtnVal = true;
            }
            return rtnVal;
        }
        public bool ApproveReview(ReviewApproveRequest req)
        {
            bool rtnVal = false;
            ReviewMaster tmpReview=_context.ReviewMaster.Find(req.MasterID);
            if (tmpReview != null)
            {
                tmpReview.ApprovedBy = req.ApprovedBy;
                tmpReview.IsApproved = true;
                tmpReview.ApprovedDate= req.ApprovedDate;
                _context.SaveChanges();
                rtnVal = true;
            }
            return rtnVal;
        }
        public IEnumerable<SendForReview> SendForReview(int BranchId)
        {
            var cutoff = DateTime.Now.AddDays(-30);
            IEnumerable<SendForReview> sfr = new List<SendForReview>();
            sfr = (from a in _context.BookedRoom.Where(b => b.BranchId == BranchId && b.isCheckout==false && b.CheckOut >= cutoff).ToList()
                   select new SendForReview
                   {
                       BookedRoomId=a.BookedRoomId,
                       BookingId=a.BookingId,
                       BookingTypeId=a.RoomTypeId,
                       CheckIn=a.CheckIn,
                       CheckOut=a.CheckOut,
                       RoomNumber=a.RoomNumber,
                       RoomTypeName=a.RoomTypeName,
                       GuestId=_context.Booking.Where(bk=>bk.BookingId==a.BookingId).SingleOrDefault().GuestId,
                       GuestName= _context.Booking.Where(bk => bk.BookingId == a.BookingId).SingleOrDefault().GuestName,
                       GuestEmail=_context.Guests.Where(g=>g.GuestId==_context.Booking.Where(b=>b.BookingId==a.BookingId).FirstOrDefault().GuestId).SingleOrDefault().email,
                       GuestPhone= _context.Guests.Where(g => g.GuestId == _context.Booking.Where(b => b.BookingId == a.BookingId).FirstOrDefault().GuestId).SingleOrDefault().Phone,
                       
                       BranchId = BranchId
                   }).ToList<SendForReview>();
            return sfr;
        }
    }
}