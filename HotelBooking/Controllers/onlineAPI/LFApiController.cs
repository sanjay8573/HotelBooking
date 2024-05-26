using HotelBooking.Model;
using HotelBooking.Model.onlineAPI;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers.onlineAPI
{
    public class LFApiController : ApiController
    {
        private readonly IOnline _online;
        private readonly ICoupon _coupon;
        public LFApiController() { 
        _online= new OnlineRepository();
         _coupon= new CouponRepository();
        }

        [Route("api/online/GetAvailableRooms")]
        [HttpPost]
        public RoomResponse GetAvailableRooms(RoomRequest roomRequest)
        {
            RoomResponse rr= _online.getAvailableRooms(roomRequest);
            return rr;
        }

        [HttpPost]
        [Route("api/online/CreateBooking")]
        public BookingResponse CreateBooking(BookingRequest bookingEntity)
        {
            BookingResponse _bookingResp=new BookingResponse();

            _bookingResp.isMailSend=true;
            try
            {
                _bookingResp.Errors = new Error { Code="000",Description="No Error"  }; 
               
                _bookingResp.BookingReference= _online.CreateOnlineBooking(bookingEntity);
            }
            catch (Exception e)
            {

                _bookingResp.BookingReference = "";
               
                _bookingResp.Errors = new Error { Code = "1001", Description = e.Message };
            }
             
            return _bookingResp;
        }
        [HttpPost]
        [Route("api/online/ApplyCoupon")]
        public CouponValidationResponse ApplyCoupon(CouponRequest CouponReq)
        {
            CouponValidationResponse _cvr = new CouponValidationResponse();
            CouponResponse _cr = new CouponResponse();
            try
            {
               
                _cr = _coupon.ApplyCoupon(CouponReq.CouponCode);
                _cvr.isValidCoupon = true;
                _cvr.CouponCode=CouponReq.CouponCode;
                _cvr.CouponDiscountAmount = _cr.CouponValue.ToString();
                _cvr.Errors = new Error { Code = "000", Description = "No Error" };
               
            }
            catch (Exception e)
            {
                _cvr.isValidCoupon = false;
                _cvr.CouponCode = CouponReq.CouponCode;
                _cvr.CouponDiscountAmount = "0.00";
                _cvr.Errors = new Error { Code = "2001", Description = e.Message};
               
            }

        

            
            return _cvr;
        }

    }
}
