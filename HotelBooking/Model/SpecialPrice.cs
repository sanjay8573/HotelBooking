using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    [Table("SpecialPriceManager")]
    public class SpecialPrice
    {
        [Key]
        public int SpecialPriceManageId { get; set; }
        public string Title { get; set; }
        public string DateRange { get; set; }
        public string RoomTypeTitle { get; set; }
        public int RoomTypeId { get; set; }
        [Column("SUN")]
        public decimal SUN1 { get; set; }
        [Column("MON")]
        public decimal MON1 { get; set; }
        [Column("TUE")]
        public decimal TUE1 { get; set; }
        [Column("WED")]
        public decimal WED1 { get; set; }
        [Column("THUR")]
        public decimal THUR1 { get; set; }
        [Column("FRI")]

        public decimal FRI1 { get; set; }
        [Column("SAT")]
        public decimal SAT1 { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int BranchId { get; set; }
    }
}