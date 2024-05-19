using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        public int BookingTypeId { get; set; }
        public string BookingTypeName { get; set; }
        public string RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public string CheckIn { get; set; }
        public string ChildAge1 { get; set; }
        public string ChildAge2 { get; set; }
        public string ChildAge3 { get; set; }
        public string Checkout { get; set; }
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

        public List<BookingCost> AllNights { get; set; }
        public string PaidServices { get; set; }

    }
}
