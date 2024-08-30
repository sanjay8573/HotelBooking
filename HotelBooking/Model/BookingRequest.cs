using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using HotelBooking.Model.onlineAPI;
using System;

namespace HotelBooking.Model
{
    public class BookingRequest
    {
        public BookingRequest()
        {
            AllNights = new List<BookingCost>();
            
        }
        public int BookingId { get; set; }
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string GuestContactNumber { get; set; }
        public string GuestEmail { get; set; }
        public int BookingTypeId { get; set; }
        public string BookingTypeName { get; set; }
        public string RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public DateTime CheckIn { get; set; }
        public string ChildAge1 { get; set; }
        public string ChildAge2 { get; set; }
        public string ChildAge3 { get; set; }
        public DateTime Checkout { get; set; }
        public int NoOfRooms { get; set; }
        public int Nights { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal PayableAmount { get; set; }
        public int BookingSourceId { get; set; }
        public double CommissionPaid { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponAmount { get; set; }
        public string BookingNumber { get; set; }
        public int BranchId { get; set; }
        public string BookingChannel { get; set; }
        public List<BookingCost> AllNights { get; set; }
        public string PaidServices { get; set; }
        public bool MailRequired { get; set; } = false;

    }
    public class BookingResponse
    {
        public Error Errors { get; set; }
       
        public string BookingReference { get; set; }
        public bool isMailSend { get; set; }
    }
    public class CouponValidationResponse
    {
        public Error Errors { get; set; }
        public string CouponCode { get; set; }

        public string CouponDiscountAmount { get; set; }
        public bool isValidCoupon { get; set; }
    }
    public class CouponRequest
    {
      

        public int BranchId { get; set; }
        public string CouponCode  { get; set; }
    }
}
