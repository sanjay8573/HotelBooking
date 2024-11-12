using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Hall
{
    [Table("HallBookingCost")]
    public class HallBookingCost
    {
        [Key]
        public int HallBookingCostId { get; set; }
        public int HallBookingId { get; set; }
        public int HallId { get; set; }
        public string Description { get; set; }
        public DateTime DATE { get; set; }
        public decimal COST { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal TAX { get; set; }
        public decimal TaxAmount { get; set; }
        public string Status { get; set; }
        public int BranchId { get; set; }

    }
}