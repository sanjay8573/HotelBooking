using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Hall
{
    [Table("HallBookingPayment")]
    public class HallBookingPayment
    {
        [Key]
        public int HallBookingPaymentId { get; set; }
        public int HallBookingId { get; set; }
        public int HallId { get; set; }
        public string Description { get; set; }
        public DateTime paymentDate { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceNumber { get; set; }
        public int BranchId { get; set; }

    }
}