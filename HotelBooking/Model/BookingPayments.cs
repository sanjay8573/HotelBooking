using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("BookingPayments")]
    public class BookingPayments
    {
        [Key]
        public int BookingPaymentId { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string InvoiceNumber { get; set; }
        public Decimal paymentAmount { get; set; }
        public Decimal paymentDue { get; set; }
        public Decimal paymentReturn { get; set; }
        public int BranchId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string OrderNumber { get; set; }
        public int PaymentType { get; set; }
        public int PaymentFor{ get; set; }
        public int Table_Room_Number { get; set; }
        public string ServiceType { get; set; }
        public string PaymentTypeName { get; set; }
        public int PBookingId { get; set; }
        public string Remarks { get; set; }
        [NotMapped]
        public Decimal Amountpaid { get; set; }
        [NotMapped]
        public Decimal AmountPending { get; set; }
    }
}