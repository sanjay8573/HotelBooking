using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Tour
{
    [Table("TourBooking")]
    public class Tour
    {
        [Key]
        public int TourBookingId { get; set; }
        public int GuestId { get; set; }
        public string GuestName {get; set; }
        public DateTime BookingDate { get; set; }
        public double TotalAmount { get; set; }
        public double TotalTax { get; set; }
        public double PayableAmount { get; set; }
        public double Discount { get; set; }
        public string CouponCode { get; set; }
        public double CouponAmount { get; set; }
        public int BookingId { get; set; }
        public string BookingNumber { get; set; }
        public string RoomNumber { get; set; }
        public string SelectedServices { get; set; }
        public string BookingChannel { get; set; }
        public string BookingStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; } = true;
        public bool isDeleted { get; set; } = false;




    }
}