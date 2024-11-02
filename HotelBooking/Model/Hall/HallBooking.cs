using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HotelBooking.Model.Hall
{
    [Table("HallBooking")]
    public class HallBooking
    {
        [Key]
        public int HallBookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int Number_Of_Person { get; set; }
        public int HallId { get; set; }
        public string HallName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public string HallBookingDetails { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal PayableAmount { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public DateTime BookingDate { get; set; }



    }
    [Table("HallBookingDetails")]
    public class HallBookingDetails
    {
        [Key]
        public int HallBookingDetailId { get; set; }
        public int HallBookingId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string BookingMenuDetail { get; set; }

        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}