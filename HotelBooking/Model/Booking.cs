﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Model
{
    [Table("Booking")]
    public class Booking
    {
        public Booking() {
            AllNights = new List<BookingCost>();
        }
        [Key]
        public int BookingId {get;set;}
        public string BookingNumber { get; set; }
        public int GuestId {get;set;}
        public string GuestName {get;set;}
        
        public int BookingTypeId {get;set;}
        public string BookingTypeName {get;set;}
        public int RoomTypeId {get;set;}
        public string RoomTypeName {get;set;}
        public int  Adult { get;set;}
        public int Child { get;set;}
        public string CheckIn {get;set;}
        public string Checkout { get;set;}
        public int NoOfRooms {get;set;}
        public int Nights {get;set;}
        public decimal TotalAmount {get;set;}
        public decimal TotalTax {get;set;}
        public decimal PayableAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponAmount { get; set; }
        public string BookingStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime BookingDate { get; set; }
        public string PaidServices { get; set; }

        public int BranchId {get;set;}
        [NotMapped]
        public String  BookingDateTime { get; set; }
        [NotMapped]
        public List<BookingCost> AllNights { get; set; }
    }
}
